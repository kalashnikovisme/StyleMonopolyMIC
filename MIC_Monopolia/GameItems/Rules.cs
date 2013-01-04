using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace GameItems {
	static public class Rules {
		static public List<string> AllRules {
			get {
				string[] str = File.ReadAllLines(@"tasks.txt", System.Text.Encoding.Default);
				List<string> s = new List<string>();
				foreach (string st in str) {
					s.Add(st.Split('\t')[0]);
				}
				return s;
			}
		}
		
		static private List<int[]> setPoints() {
			string[] datas = File.ReadAllLines(@"tasks.txt", System.Text.Encoding.Default);
			List<int[]> dic = new List<int[]>();
			foreach (string d in datas) {
				if (d.Split('\t').Length == 1) {
					dic.Add(new int[] {});
				} else {
					dic.Add(new int[] { Int32.Parse(d.Split('\t')[1]), Int32.Parse(d.Split('\t')[2]), Int32.Parse(d.Split('\t')[3]) });
				}
			}
			return dic;
		}
		
		static public int[] Points(int taskIndex) {
			return setPoints()[taskIndex];
		}
	}
}