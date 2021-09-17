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
			settingsFile = settingsPath + @"\prefs.txt";
			LoadPrefs();
		}

		public void LoadPrefs()
		{
			if (File.Exists(settingsPath))
			{
				string[] temp;
				string[] prefsLines = File.ReadAllLines(settingsFile);
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
			Directory.CreateDirectory(settingsPath);
			List<string> lines = new List<string>();
			foreach (string key in userPrefs)
			{
				lines.Add(key + "=" + userPrefs[key]);
			}
			File.WriteAllLinesAsync(settingsFile, lines);
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
