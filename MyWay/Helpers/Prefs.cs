using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyWay.Helpers
{
	class Prefs
	{
		NameValueCollection userPrefs = new NameValueCollection();
		string settingsPath = "";
		string settingsFile = "";


		public Prefs()
		{
			settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyWay").ToString();

			settingsFile = "prefs.txt";
			LoadPrefs(settingsFile);
		}

		public void LoadPrefs(string prefFile)
		{
			string fullPath = settingsPath + "\\" + prefFile;
			if (File.Exists(fullPath))
			{
				string[] temp;
				string[] prefsLines = File.ReadAllLines(fullPath);

				foreach (string aPref in prefsLines)
				{
					temp = aPref.Split("=");
					userPrefs[temp[0]] = temp[1];
				}
			}
		}

		public void SavePrefs()
		{
			// Creates if necessary, ignores if already there

			string fullPath = settingsPath + "\\" + settingsFile;
			File.WriteAllLinesAsync(fullPath, lines);

		}

		public string GetPref(string name)
		{
			return userPrefs[name];
		}

		public void SetPref(string key, string value)
		{
			userPrefs[key] = value;
			SavePrefs();
		}
	}
}
