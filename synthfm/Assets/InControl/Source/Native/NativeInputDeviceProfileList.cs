// ReSharper disable StringLiteralTypo
namespace InControl
{
	using UnityEngine;


	public class NativeInputDeviceProfileList : ScriptableObject
	{
		public static readonly string[] Profiles = new string[]
		{
			"InControl.NativeDeviceProfiles.AfterglowXbox360WindowsNativeProfile",
			"InControl.NativeDeviceProfiles.AirFloControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.AirFloPS3WindowsNativeProfile",
			"InControl.NativeDeviceProfiles.AppleMFiExtendedGamepadNativeProfile",
			"InControl.NativeDeviceProfiles.AppleMFiMicroGamepadNativeProfile",
			"InControl.NativeDeviceProfiles.ArdwiinoControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.AtPlayControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.BatarangControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.BETAOPControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.BigBenControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.BrookNeoGeoConverterMacNativeProfile",
			"InControl.NativeDeviceProfiles.BrookPS2ConverterMacNativeProfile",
			"InControl.NativeDeviceProfiles.BuffaloClassicMacNativeProfile",
			"InControl.NativeDeviceProfiles.BuffaloClassicWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.DragonRiseArcadeStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.DualShock4MFiNativeProfile",
			"InControl.NativeDeviceProfiles.EASportsControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.EightBitdoNES30ProBTMacNativeProfile",
			"InControl.NativeDeviceProfiles.EightBitdoNES30ProUSBMacNativeProfile",
			"InControl.NativeDeviceProfiles.EightBitdoNES30ProWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.EightBitdoSF30ProWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.EightBitdoSFC30MacNativeProfile",
			"InControl.NativeDeviceProfiles.EightBitdoSFC30WindowsNativeProfile",
			"InControl.NativeDeviceProfiles.EightBitdoSNES30MacNativeProfile",
			"InControl.NativeDeviceProfiles.EightBitdoSNES30WindowsNativeProfile",
			"InControl.NativeDeviceProfiles.ElecomControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.FusionXboxOneControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.GameCubeMayflashWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.GameCubeWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.GameStopControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.GoogleStadiaMacNativeProfile",
			"InControl.NativeDeviceProfiles.GoogleStadiaWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.GuitarHeroControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.HarmonixDrumKitMacNativeProfile",
			"InControl.NativeDeviceProfiles.HarmonixGuitarMacNativeProfile",
			"InControl.NativeDeviceProfiles.HarmonixKeyboardMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoneyBeeControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriBlueSoloControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriDOA4ArcadeStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriEdgeFightingStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriEX2ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.HORIFightingCommanderControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriFightingCommanderMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriFightingEdgeArcadeStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriFightingStickEX2MacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriFightingStickMiniMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriFightingStickVXMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriFightStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriPadEXTurboControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriPadUltimateMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriRAPNFightingStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriRealArcadeProEXMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriRealArcadeProEXPremiumVLXMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriRealArcadeProEXSEMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriRealArcadeProHayabusaMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriRealArcadeProIVMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriRealArcadeProVHayabusaMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriRealArcadeProVKaiFightingStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriRealArcadeProVXMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriRealArcadeProVXSAMacNativeProfile",
			"InControl.NativeDeviceProfiles.HoriXbox360GemPadExMacNativeProfile",
			"InControl.NativeDeviceProfiles.HyperkinX91MacNativeProfile",
			"InControl.NativeDeviceProfiles.InjusticeFightStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.IonDrumRockerMacNativeProfile",
			"InControl.NativeDeviceProfiles.JoytekXbox360ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.KiwitataNESWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.KonamiDancePadMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechChillStreamControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechDriveFXRacingWheelMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF310ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF310ModeDMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF310ModeDWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF310ModeXWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF510ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF510ModeDMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF510ModeDWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF510ModeXWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF710ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF710ModeDMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF710ModeDWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechF710ModeXWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechG920RacingWheelMacNativeProfile",
			"InControl.NativeDeviceProfiles.LogitechThunderpadMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzArcadeStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzBeatPadMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzBrawlStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzCODControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzFightPadControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzFightPadMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzFightStickTE2MacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzFightStickTESPlusMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzFPSProMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzGhostReconFightingStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzInnoControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzMC2ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzMC2RacingWheelMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzMicroConControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzMicroControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzMLGFightStickTEMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzNeoFightStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzPortableDrumMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzProControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzSaitekAV8R02MacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzSF4FightStickRound2TEMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzSF4FightStickSEMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzSF4FightStickTEMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzSoulCaliberFightStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzSSF4ChunLiFightStickTEMacNativeProfile",
			"InControl.NativeDeviceProfiles.MadCatzSSF4FightStickTEMacNativeProfile",
			"InControl.NativeDeviceProfiles.MatCatzControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MayflashMagicNSMacNativeProfile",
			"InControl.NativeDeviceProfiles.McbazelAdapterMacNativeProfile",
			"InControl.NativeDeviceProfiles.MicrosoftAdaptiveControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MicrosoftXbox360ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MicrosoftXboxControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MicrosoftXboxOneControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MicrosoftXboxOneEliteControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MKKlassikFightStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.MLGControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.MVCTEStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.NaconGC100XFControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.NintendoSwitchProMacNativeProfile",
			"InControl.NativeDeviceProfiles.NintendoSwitchProWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.NVidiaShieldWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.OUYAWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.PCTWINSHOCKWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.PDPAfterglowControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PDPAfterglowPrismaticControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PDPBattlefieldXBoxOneControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PDPMarvelControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PDPMetallicsLEControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PDPTitanfall2XboxOneControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PDPTronControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PDPVersusControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PDPXbox360ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PDPXboxOneArcadeStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.PDPXboxOneControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PlayStation3MacNativeProfile",
			"InControl.NativeDeviceProfiles.PlayStation4MacNativeProfile",
			"InControl.NativeDeviceProfiles.PlayStation4WindowsNativeProfile",
			"InControl.NativeDeviceProfiles.PowerAAirflowControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.POWERAFUS1ONTournamentControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PowerAMiniControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PowerAMiniProExControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PowerAMiniXboxOneControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.PowerANintendoSwitchMacNativeProfile",
			"InControl.NativeDeviceProfiles.PowerASpectraIlluminatedControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.ProEXXbox360ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.ProEXXboxOneControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.QanbaFightStickPlusMacNativeProfile",
			"InControl.NativeDeviceProfiles.RazerAtroxArcadeStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.RazerOnzaControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.RazerOnzaTEControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.RazerSabertoothEliteControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.RazerServalWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.RazerStrikeControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.RazerWildcatControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.RazerWolverineUltimateControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.RedOctaneControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.RedOctaneGuitarMacNativeProfile",
			"InControl.NativeDeviceProfiles.RockBandDrumsMacNativeProfile",
			"InControl.NativeDeviceProfiles.RockBandGuitarMacNativeProfile",
			"InControl.NativeDeviceProfiles.RockCandyControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.RockCandyPS3ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.RockCandyXbox360ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.RockCandyXboxOneControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.SaitekXbox360ControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.SteamWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.SteelseriesNimbusMacNativeProfile",
			"InControl.NativeDeviceProfiles.ThrustMasterFerrari430RacingWheelMacNativeProfile",
			"InControl.NativeDeviceProfiles.ThrustmasterFerrari458RacingWheelMacNativeProfile",
			"InControl.NativeDeviceProfiles.ThrustMasterFerrari458SpiderRacingWheelMacNativeProfile",
			"InControl.NativeDeviceProfiles.ThrustmasterGPXControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.ThrustmasterTMXMacNativeProfile",
			"InControl.NativeDeviceProfiles.ThrustmasterTXGIPMacNativeProfile",
			"InControl.NativeDeviceProfiles.TrustPredatorJoystickMacNativeProfile",
			"InControl.NativeDeviceProfiles.TSZPelicanControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.Xbox360MortalKombatFightStickMacNativeProfile",
			"InControl.NativeDeviceProfiles.Xbox360ProEXControllerMacNativeProfile",
			"InControl.NativeDeviceProfiles.Xbox360WiredWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.Xbox360WirelessWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.XboxOneEliteWindows10AENativeProfile",
			"InControl.NativeDeviceProfiles.XboxOneEliteWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.XboxOneMFiNativeProfile",
			"InControl.NativeDeviceProfiles.XboxOneSMacNativeProfile",
			"InControl.NativeDeviceProfiles.XboxOneWindows10AENativeProfile",
			"InControl.NativeDeviceProfiles.XboxOneWindows10NativeProfile",
			"InControl.NativeDeviceProfiles.XboxOneWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.XboxOneWirelessAdapterWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.XInputWindowsNativeProfile",
			"InControl.NativeDeviceProfiles.XTR_G2_MacNativeProfile",
			"InControl.NativeDeviceProfiles.XTR_G2_WindowsNativeProfile",
			"InControl.NativeDeviceProfiles.XTR55_G2_MacNativeProfile",
			"InControl.NativeDeviceProfiles.XTR55_G2_WindowsNativeProfile",
		};
	}
}
