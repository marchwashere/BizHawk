﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

using BizHawk.Emulation.Common;

namespace BizHawk.Client.Common
{
	public interface ITasMovie : IMovie, INotifyPropertyChanged
	{
		bool BindMarkersToInput { get; set; }
		bool LastPositionStable { get; set; }

		IMovieChangeLog ChangeLog { get; }
		IStateManager TasStateManager { get; }
		Func<string> ClientSettingsForSave { set; }
		Action<string> GetClientSettingsOnLoad { set; }
		ITasMovieRecord this[int index] { get; }
		ITasSession Session { get; }
		TasMovieMarkerList Markers { get; }
		ITasBranchCollection Branches { get; }
		TasLagLog LagLog { get; }
		IStringLog VerificationLog { get; }
		int LastEditedFrame { get; }

		string DisplayValue(int frame, string buttonName);
		void FlagChanges();
		void ClearChanges();
		IStringLog GetLogEntries();

		void GreenzoneCurrentFrame();
		void ToggleBoolState(int frame, string buttonName);
		void SetAxisState(int frame, string buttonName, int val);
		void SetAxisStates(int frame, int count, string buttonName, int val);
		void SetBoolState(int frame, string buttonName, bool val);
		void SetBoolStates(int frame, int count, string buttonName, bool val);
		void InsertInput(int frame, string inputState);
		void InsertInput(int frame, IEnumerable<string> inputLog);
		void InsertInput(int frame, IEnumerable<IController> inputStates);

		void InsertEmptyFrame(int frame, int count = 1);
		int CopyOverInput(int frame, IEnumerable<IController> inputStates);

		void RemoveFrame(int frame);
		void RemoveFrames(ICollection<int> frames);
		void RemoveFrames(int removeStart, int removeUpTo);
		void SetFrame(int frame, string source);

		void LoadBranch(TasBranch branch);

		void CopyLog(IEnumerable<string> log);
		void CopyVerificationLog(IEnumerable<string> log);
	}
}
