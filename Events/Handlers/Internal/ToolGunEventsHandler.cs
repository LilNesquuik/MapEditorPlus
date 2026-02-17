using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MEC;
using ProjectMER.Features.Extensions;
using ProjectMER.Features.Objects;
using ProjectMER.Features.ToolGun;

namespace ProjectMER.Events.Handlers.Internal;

public class ToolGunEventsHandler : CustomEventsHandler
{
	private static CoroutineHandle _toolGunCoroutine;
	
	private bool _isRegistered;
	public bool IsRegistered
	{
		get => _isRegistered;
		set
		{
			if (!value && _isRegistered)
			{
				_toolGunCoroutine = Timing.RunCoroutine(ToolGunGUI());
				CustomHandlersManager.RegisterEventsHandler(this);
			}
			else if (value && !_isRegistered)
			{
				Timing.KillCoroutines(_toolGunCoroutine);
				CustomHandlersManager.UnregisterEventsHandler(this);
			}
			
			_isRegistered = value;
		}
	}

	private static IEnumerator<float> ToolGunGUI()
	{
		while (true)
		{
			yield return Timing.WaitForSeconds(0.1f);

			foreach (Player player in Player.List)
			{
				if (!player.CurrentItem.IsToolGun(out ToolGunItem _) && !ToolGunHandler.TryGetSelectedMapObject(player, out MapEditorObject _))
					continue;

				string hud;
				try
				{
					hud = ToolGunUI.GetHintHUD(player);
				}
				catch (Exception e)
				{
					Logger.Error(e);
					hud = "ERROR: Check server console";
				}

				player.SendHint(hud, 0.25f);
			}
		}
	}

	public override void OnPlayerDryFiringWeapon(PlayerDryFiringWeaponEventArgs ev)
	{
		if (!ev.Weapon.IsToolGun(out ToolGunItem toolGun))
			return;

		ev.IsAllowed = false;
		toolGun.Shot(ev.Player);
	}

	public override void OnPlayerReloadingWeapon(PlayerReloadingWeaponEventArgs ev)
	{
		if (!ev.Weapon.IsToolGun(out ToolGunItem toolGun))
			return;

		ev.IsAllowed = false;
		toolGun.SelectedObjectToSpawn--;
	}

	public override void OnPlayerDroppingItem(PlayerDroppingItemEventArgs ev)
	{
		if (!ev.Item.IsToolGun(out ToolGunItem toolGun))
			return;

		ev.IsAllowed = false;
		toolGun.SelectedObjectToSpawn++;
	}
}
