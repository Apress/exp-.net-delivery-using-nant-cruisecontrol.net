using System;
using System.Collections;
using System.Reflection;

namespace Etomic.NAntExtensions
{
	[Serializable]
	public class ObjectComparer : IComparer
	{
		private string[] _fields;
		private bool[] _descending;

		public ObjectComparer(params string[] fields) : this(fields, new bool[fields.Length]) {}

		public ObjectComparer(string[] fields, bool[] descending)
		{
			this._fields = fields;
			this._descending = descending;
		}

		public int Compare(object x, object y)
		{
			for(int i = 0; i<this._fields.Length; i++)
			{
				PropertyInfo pix = x.GetType().GetProperty(this._fields[i]);
				PropertyInfo piy = y.GetType().GetProperty(this._fields[i]);

				IComparable pvalx = (IComparable)pix.GetValue(x, null);
				object pvaly = piy.GetValue(y, null);

				int result = pvalx.CompareTo(pvaly);
				if (result != 0)
				{
					if (this._descending[i])
					{
						return -result;
					}
					else
					{
						return result;
					}
				}
			}
			return 0;
		}
	}
}

