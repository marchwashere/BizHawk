using System;

using BizHawk.Common;

namespace BizHawk.Emulation.Common
{
	/// <summary>
	/// This object facilitates communications between client and core
	/// The primary use is to provide a client => core communication, such as providing client-side callbacks for a core to use
	/// Any communications that can be described as purely a Core -> Client system, should be provided as an <seealso cref="IEmulatorService"/> instead
	/// It is important that by design this class stay immutable
	/// </summary>
	public class CoreComm
	{
		private readonly IDialogParent _dialogParent;

		private readonly Action<string> _osdMessageCallback;

		public CoreComm(
			ICoreFileProvider coreFileProvider,
			CorePreferencesFlags prefs,
			IDialogParent dialogParent,
			Action<string> osdMessageCallback)
		{
			CoreFileProvider = coreFileProvider;
			CorePreferences = prefs;
			_dialogParent = dialogParent;
			_osdMessageCallback = osdMessageCallback;
		}

		public ICoreFileProvider CoreFileProvider { get; }

		/// <summary>Displays a message in a modal dialog. Reasonably annoying, so shouldn't be used most of the time.</summary>
		public void ShowMessage(string message) => _dialogParent.ModalMessageBox(message, "Warning", EMsgBoxIcon.Warning);

		/// <summary>Displays a message on the OSD. Less annoying, so can be used for ignorable helpful messages.</summary>
		public void Notify(string message) => _osdMessageCallback(message);

		[Flags]
		public enum CorePreferencesFlags
		{
			None = 0,
			WaterboxCoreConsistencyCheck = 1,
			WaterboxMemoryConsistencyCheck = 2
		}

		/// <summary>
		/// Yeah, I put more stuff in corecomm. If you don't like it, change the settings/syncsettings stuff to support multiple "settings sets" to act like ini file sections kind of, so that we can hand a generic settings object to cores instead of strictly ones defined by the cores
		/// </summary>
		public CorePreferencesFlags CorePreferences { get; }
	}
}
