using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace Sommite.Extensions
{
	public static class SystemTypes
	{
		#region Decimal

        public static decimal TruncateAfter(this decimal value, int positions)
        {
            string _str = value.ToString();
            if (_str.Contains(",") || _str.Contains("."))
            {
                int _start = _str.IndexOf(",");
                if (_start == -1)
                {
                    _start = _str.IndexOf(".");
                }
                string _strTruncated = null;
                if (_str.Length > positions + _start)
                {
                    _strTruncated = _str.Remove(_start + positions + 1, _str.Length - (_start + positions) - 1);
                }
                else
                {
                    _strTruncated = value.ToString();
                }
                return Convert.ToDecimal(_strTruncated);
            }
            else
            {
                return Convert.ToDecimal(_str);
            }

        }

        public static string ToDateString(this string s)
        {
            try
            {
                return string.Format("{0}-{1}-{2}", s.Substring(0, 4), s.Substring(4, 2), s.Substring(6, 2));

            }
            catch { return s; }

        }

        public static decimal Round2(this decimal value)
        {
            return Convert.ToDecimal(Math.Round(value, 2));
        }

        public static decimal Round6(this decimal value)
        {
            return Convert.ToDecimal(Math.Round(value, 6));
        }

        public static decimal? Round2(this decimal? value)
        {
            if (value.HasValue)
            {
                return Math.Round(value.Value, 2);
            }
            else
            {
                return value;
            }
        }

        //ToDecimal
		public static decimal ToDecimal(this string o)
		{
			decimal? retval = null;

			try
			{
				retval = Convert.ToDecimal(o);
			}
			catch (Exception ex)
			{
				throw new Exception("Conversion from " + o + " to decimal is not supported", ex) ;
			}

			return retval.Value;
		}
		#endregion

		#region String

		/// <summary>
		/// Returns the virtual path from a physical path, useful for creating links
		/// </summary>
		/// <param name="fullServerPath"></param>
		/// <param name="applicationPath">You can retrieve the application path by calling HttpContext.Current.Request.PhysicalApplicationPath</param>
		/// <returns></returns>
		public static string ToVirtualPath(this string fullServerPath, string applicationPath)
		{
			return @"~\" + fullServerPath.Replace(applicationPath, String.Empty);
		}

		public static string ToPartialPath(this string fullServerPath, string applicationPath)
		{
			return fullServerPath.Replace(applicationPath, String.Empty);
		}

		//ToString()
		public static string AddString(this string s,string addedString, string delimiter = "\n")
		{
			return string.Format("{0}{1}{2}", s, delimiter, addedString);
		}

		public static bool LowerContains(this string s,string searchString)
		{
			return s.ToLower().Contains(searchString.ToLower());
		}

		public static string ToArrayString(this string[] list)
		{
			string retval = "";

			foreach (var item in list)
			{
				retval += item + ",";
			}

			if (!string.IsNullOrEmpty(retval) && retval.Last() == ',') retval = retval.Remove(retval.Length - 1, 1);

			return retval;
		}

		public static bool IsNumeric(this string s)
		{
			int i;
			return int.TryParse(s, out i);
		}

		public static string ToProperCase(this string s)
		{
			string[] words = s.Split(' ');
			List<string> convertedWords = new List<string>();
			foreach (string word in words)
			{
				convertedWords.Add(word.Substring(0, 1).ToUpper() + word.Substring(1, word.Length - 1));
			}
			string retval = "";

			foreach (var converted in convertedWords)
			{
				retval += converted + " ";
			}

			return retval.TrimEnd(' ');
		}

		public static bool IsEmpty(this string s)
		{
			if (s == null)
				return true;
			else
				return string.IsNullOrEmpty(s);
		}

        public static string ToDecimalString(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "0";
            }
            try
            {
                switch (System.Globalization.CultureInfo.CurrentUICulture.LCID.ToString())
                {
                    case "1033":
                        return value.Replace(".", ",");
                    default:
                        return value.Replace(".", ",");
                }
            }
            catch
            {
                return "";
            }
        }

        public static decimal ToDecimalFromString(this string value)
        {
            try
            {
                return Convert.ToDecimal(value.ToDecimalString());
            }
            catch
            {
                return 0M;
            }
        }

		#endregion

		#region Int

		public static DateTime FirstMondayInYear(this int Year)
		{
			// get the date for the 4-Jan for this year
			DateTime dt = new DateTime(Year, 1, 4);

			// get the ISO day number for this date 1==Monday, 7==Sunday
			int dayNumber = (int)dt.DayOfWeek; // 0==Sunday, 6==Saturday
			if (dayNumber == 0)
			{
				dayNumber = 7;
			}

			// return the date of the Monday that is less than or equal
			// to this date
			return dt.AddDays(1 - dayNumber);
		}

		public static int ToInt(this string i)
		{
			int? retval = null;

			try
			{
				retval = Convert.ToInt32(i);
			}
			catch (Exception ex)
			{
				throw new Exception("Conversion from " + i + " to Int32 is not supported", ex);
			}

			return retval.Value;
		}
		#endregion

		#region DateTime

		public static DateTime MondayInWeek(this DateTime dt)
		{
			// get the date for the 4-Jan for this year
			
			// get the ISO day number for this date 1==Monday, 7==Sunday
			int dayNumber = (int)dt.DayOfWeek; // 0==Sunday, 6==Saturday
			if (dayNumber == 0)
			{
				dayNumber = 7;
			}

			// return the date of the Monday that is less than or equal
			// to this date
			return dt.AddDays(1 - dayNumber);
		}

		public static YearWeek IsoWeek(this DateTime dt)
		{
			DateTime week1;
			int IsoYear = dt.Year;
			if (dt >= new DateTime(IsoYear, 12, 29))
			{
				week1 = (IsoYear + 1).FirstMondayInYear();
				if (dt < week1)
				{
					week1 = FirstMondayInYear(IsoYear);
				}
				else
				{
					IsoYear++;
				}
			}
			else
			{
				week1 = FirstMondayInYear(IsoYear);
				if (dt < week1)
				{
					week1 = FirstMondayInYear(--IsoYear);
				}
			}

			return new YearWeek() { Year = (IsoYear), Week = ((dt - week1).Days / 7 + 1), Monday = dt.MondayInWeek()};
		}

        public static YearWeek IsoWeek(this int year, int week)
        {
            DateTime week1;
            week1 = FirstMondayInYear(year);
            var dt = week1.AddDays(7 * (week - 1));
            return new YearWeek() { Year = (year), Week = week, Monday = dt.MondayInWeek() };
        }

		public static YearWeek PreviousIsoWeek(this YearWeek wk)
		{
			return wk.Monday.AddDays(-7).IsoWeek(); ;
		}

		public static YearWeek NextIsoWeek(this YearWeek wk)
		{
			return wk.Monday.AddDays(7).IsoWeek(); ;
		}

		public static string ToYYYYMMDD(this DateTime dt)
		{
			return String.Format("{0}{1}{2}", dt.Year, dt.Month.ToString().PadLeft(2, '0'), dt.Day.ToString().PadLeft(2, '0'));
		}

		public static DateTime FromYYYMMMDD(this string dt)
		{
			DateTime? retval = null;
			try
			{
				retval = String.Format("{0}-{1}-{2}", dt.Substring(0, 4), dt.Substring(4, 2), dt.Substring(6, 2)).ToDateTime();
			}
			catch(Exception ex)
			{
				throw new Exception("Conversion from " + dt + " to datetime is not supported", ex);
			}
			return retval.Value;
		}

		public static DateTime ToDateTime(this string s)
		{
			DateTime? retval = null;

			try
			{
				retval = Convert.ToDateTime(s);
			}
			catch (Exception ex)
			{
				throw new Exception("Conversion from " + s + " to datetime is not supported", ex);
			}

			return retval.Value;
		}

		public static string ToShortDateString(this DateTime? d, string Language = "NL")
		{
			if (d.HasValue)
			{
				return d.Value.ToShortDateString();
			}
			else
			{
				switch (Language)
				{
					case "NL":
						return "Geen datum bekend";
					case "EN":
						return "No date specified";
					default:
						return "No date specified";
				}
				
			}
		}
		
		#endregion

		#region Lists

        public static IEnumerable<T> Add<T>(this IEnumerable<T> sequence, T item)
        {
            return (sequence ?? Enumerable.Empty<T>()).Concat(new[] { item });
        }

        public static T[] AddRangeToArray<T>(this T[] sequence, T[] items)
        {
            return (sequence ?? Enumerable.Empty<T>()).Concat(items).ToArray();
        }

        public static T[] AddToArray<T>(this T[] sequence, T item)
        {
            return Add(sequence, item).ToArray();
        }

        public static void ToCSV<T>(this IList<T> list, string path = "", string listName = "", string include = "", string exclude = "")
        {

            var now = DateTime.Now;

            if (path == "")
            {
                path = string.Format(@"c:\temp\{0}-{1}{2}{3}-{4}{5}{6}{7}.csv", (listName == "" ? list.GetType().Name : listName), now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
            }

            if (path != "")
            {
                CreateCsvFile(list, path, include, exclude);
            }
        }

        private static string CreateCsvFile<T>(IList<T> list, string path, string include, string exclude)
        {
            //Variables for build CSV string
            StringBuilder sb = new StringBuilder();
            List<string> propNames;
            List<string> propValues;
            bool isNameDone = false;

            //Get property collection and set selected property list
            PropertyInfo[] props = typeof(T).GetProperties();
            List<PropertyInfo> propList = GetSelectedProperties(props, include, exclude);

            //Add list name and total count
            string typeName = GetSimpleTypeName(list);
            sb.AppendLine(string.Format("{0} List - Total Count: {1}", typeName, list.Count.ToString()));

            //Iterate through data list collection
            foreach (var item in list)
            {
                sb.AppendLine("");
                propNames = new List<string>();
                propValues = new List<string>();

                //Iterate through property collection
                foreach (var prop in propList)
                {
                    //Construct property name string if not done in sb
                    if (!isNameDone) propNames.Add(prop.Name);

                    //Construct property value string with double quotes for issue of any comma in string type data
                    var val = prop.PropertyType == typeof(string) ? "\"{0}\"" : "{0}";
                    try
                    {
                        propValues.Add(string.Format(val, prop.GetValue(item, null)));
                    }
                    catch { propValues.Add(""); }
                }
                //Add line for Names
                string line = string.Empty;
                if (!isNameDone)
                {
                    line = string.Join(",", propNames);
                    sb.AppendLine(line);
                    isNameDone = true;
                }
                //Add line for the values
                line = string.Join(",", propValues);
                sb.Append(line);
            }
            if (!string.IsNullOrEmpty(sb.ToString()) && path != "")
            {
                File.WriteAllText(path, sb.ToString());
            }
            return path;
        }

        private static List<PropertyInfo> GetSelectedProperties(PropertyInfo[] props, string include, string exclude)
        {
            List<PropertyInfo> propList = new List<PropertyInfo>();
            if (include != "") //Do include first
            {
                var includeProps = include.ToLower().Split(',').ToList();
                foreach (var item in props)
                {
                    var propName = includeProps.Where(a => a == item.Name.ToLower()).FirstOrDefault();
                    if (!string.IsNullOrEmpty(propName))
                        propList.Add(item);
                }
            }
            else if (exclude != "") //Then do exclude
            {
                var excludeProps = exclude.ToLower().Split(',');
                foreach (var item in props)
                {
                    var propName = excludeProps.Where(a => a == item.Name.ToLower()).FirstOrDefault();
                    if (string.IsNullOrEmpty(propName))
                        propList.Add(item);
                }
            }
            else //Default
            {
                propList.AddRange(props.ToList());
            }
            return propList;
        }

        private static string GetSimpleTypeName<T>(IList<T> list)
        {
            string typeName = list.GetType().ToString();
            try
            {
                int pos = typeName.IndexOf("[") + 1;
                typeName = typeName.Substring(pos, typeName.LastIndexOf("]") - pos);
                typeName = typeName.Substring(typeName.LastIndexOf(".") + 1);
            }
            catch { }
            return typeName;
        }

		public static string ToArrayString(this List<string> list)
		{
			string retval = "";

			foreach (var item in list)
			{
				retval += item + ",";
			}

			if (!string.IsNullOrEmpty(retval) && retval.Last() == ',') retval = retval.Remove(retval.Length - 1, 1);

			return retval;
		}
		#endregion

		#region Bitmaps

		public static Image ResizeImage(this Image orginalImage, double resizeBy)
		{
			var originalWidth = orginalImage.Width;
			var originalHeight = orginalImage.Height;

			var canvasWidth = (int)(originalWidth*resizeBy);
			var canvasHeight = (int)(originalHeight*resizeBy);

			System.Drawing.Image thumbnail =
				new Bitmap(canvasWidth, canvasHeight); // changed parm names
			System.Drawing.Graphics graphic =
						 System.Drawing.Graphics.FromImage(thumbnail);

			graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphic.SmoothingMode = SmoothingMode.HighQuality;
			graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphic.CompositingQuality = CompositingQuality.HighQuality;

			/* ------------------ new code --------------- */

			// Figure out the ratio
			double ratioX = (double)canvasWidth / (double)originalWidth;
			double ratioY = (double)canvasHeight / (double)originalHeight;
			// use whichever multiplier is smaller
			double ratio = ratioX < ratioY ? ratioX : ratioY;

			// now we can get the new height and width
			int newHeight = Convert.ToInt32(originalHeight * ratio);
			int newWidth = Convert.ToInt32(originalWidth * ratio);

			// Now calculate the X,Y position of the upper-left corner 
			// (one of these will always be zero)
			int posX = Convert.ToInt32((canvasWidth - (originalWidth * ratio)) / 2);
			int posY = Convert.ToInt32((canvasHeight - (originalHeight * ratio)) / 2);

			graphic.Clear(Color.White); // white padding
			graphic.DrawImage(orginalImage, posX, posY, newWidth, newHeight);

			/* ------------- end new code ---------------- */

			System.Drawing.Imaging.ImageCodecInfo[] info =
							 ImageCodecInfo.GetImageEncoders();
			EncoderParameters encoderParameters;
			encoderParameters = new EncoderParameters(1);
			encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality,
							 100L);
			//thumbnail.Save(path + newWidth + "." + originalFilename, info[1],encoderParameters);
			return thumbnail;
		}

		public static string ToBase64String(this Bitmap bmp, ImageFormat imageFormat)
		{
			string base64String = string.Empty;

			MemoryStream memoryStream = new MemoryStream();
			bmp.Save(memoryStream, imageFormat);

			memoryStream.Position = 0;
			byte[] byteBuffer = memoryStream.ToArray();

			memoryStream.Close();

			base64String = Convert.ToBase64String(byteBuffer);
			byteBuffer = null;

			return base64String;
		}

        public static Image ToImage(this Binary imgBinary)
        {
            try { return new Bitmap(new MemoryStream(imgBinary.ToArray())) ?? null; }
            catch { return null; };
        }

		public static string ToBase64(this Image image,
			System.Drawing.Imaging.ImageFormat format)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				// Convert Image to byte[]
				image.Save(ms, format);
				byte[] imageBytes = ms.ToArray();

				// Convert byte[] to Base64 String
				string base64String = Convert.ToBase64String(imageBytes);
				return base64String;
			}
		}

		public static  Image ToImage(this string base64String)
		{
			// Convert Base64 String to byte[]
			byte[] imageBytes = Convert.FromBase64String(base64String);
			MemoryStream ms = new MemoryStream(imageBytes, 0,
			  imageBytes.Length);

			// Convert byte[] to Image
			ms.Write(imageBytes, 0, imageBytes.Length);
			Image image = Image.FromStream(ms, true);
			return image;
		}

	    public static byte[] ToBytes(this string base64String)
	    {
            var bytes = Convert.FromBase64String(base64String);
	        return bytes;
            
	    }

	    public static MemoryStream ToMemoryStream(this byte[] bytes)
	    {
            var ms = new MemoryStream(bytes, 0, bytes.Length);
            ms.Write(bytes, 0, bytes.Length);
            return ms;
	    }

        public static MemoryStream ToMemoryStream(this string base64String)
        {
            return base64String.ToBytes().ToMemoryStream();
        }


	    public static string ToBase64ImageTag(this Bitmap bmp, ImageFormat imageFormat)
		{
			string imgTag = string.Empty;
			string base64String = string.Empty;

			base64String = bmp.ToBase64String(imageFormat);

			imgTag = "<img src=\"data:image/" + imageFormat.ToString() + ";base64,";
			imgTag += base64String + "\" ";
			imgTag += "width=\"" + bmp.Width.ToString() + "\" ";
			imgTag += "height=\"" + bmp.Height.ToString() + "\" />";

			return imgTag;
		}
		#endregion

        #region Bytes & Arrays
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        #endregion

        public static string GetTableName<TEntity>(this Table<TEntity> myTable)
							where TEntity : class
		{
			Type type = typeof(TEntity);
			object[] temp = type.GetCustomAttributes(
								   typeof(System.Data.Linq.Mapping.TableAttribute),
								   true);
			if (temp.Length == 0)
				return null;
			else
			{
				var tableAttribute = temp[0] as System.Data.Linq.Mapping.TableAttribute;
				if (tableAttribute != null)
					return tableAttribute.Name;
				else
				{
					return null;
				}
			}
		}

		public static string GetTableName<TEntity>(this IQueryable< TEntity> myTable) where TEntity : class
		{
			string name = string.Empty;
			Type type;
			object[] attributes;

			type = typeof(TEntity);
			attributes = type.GetCustomAttributes(typeof(TableAttribute), true);

			if (attributes.Length > 0)
				name = ((TableAttribute)attributes[0]).Name;
			if (!string.IsNullOrEmpty(name))
				return name;

			return type.Name;
		}
	}

	/// <summary>
/// Represents the class public.
/// </summary>
public class YearWeek
	{
		public int Year { get; set; }
		public int Week { get; set; }
		public int YW { get { return Year * 100 + Week; } }
		public DateTime Monday { get; set; }
		public DateTime Tuesday { get { return Monday.AddDays(1); } }
		public DateTime Wednesday { get { return Monday.AddDays(2); } }
		public DateTime Thursday { get { return Monday.AddDays(3); } }
		public DateTime Friday { get { return Monday.AddDays(4); } }
		public DateTime Saturday { get { return Monday.AddDays(5); } }
		public DateTime Sunday { get { return Monday.AddDays(6); } }
		public DateTime WeekStartDate { get { return Monday; } }
		public DateTime WeekEndDate { get { return Sunday; } }

		private List<DateTime> _days = null;
		public List<DateTime> Days
		{
			get
			{
				if (_days == null)
				{
					_days = new List<DateTime>();
					_days.Add(Monday);
					_days.Add(Tuesday);
					_days.Add(Wednesday);
					_days.Add(Thursday);
					_days.Add(Friday);
					_days.Add(Saturday);
					_days.Add(Sunday);
				}

				return _days;
			}
		}

		public string DisplayName { get { return string.Format("{0}-{1} ({2} - {3})", Year, Week, WeekStartDate.ToShortDateString(), WeekEndDate.ToShortDateString()); } }

		public override string ToString()
		{
			return DisplayName;
		}

		public override bool Equals(object obj)
		{
			try
			{
				if (obj == null) return false;
				YearWeek cmp = (YearWeek)obj;
				return (YW==cmp.YW);
			}
			catch { return false; }
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

	}

	/// <summary>
/// Represents the class public.
/// </summary>
public class YearMonth
	{
		private int _year;
		public int Year { get { return _year; } }
		private int _month;
		public int Month { get { return _month; } }

		private List<DateTime> _days = new List<DateTime>();
		public List<DateTime> Days { get { return _days; } }

		private List<YearWeek> _weeks = new List<YearWeek>();
		public List<YearWeek> Weeks { get { return _weeks; } }

		public YearMonth(int Year, int Month)
		{
			_year = Year;
			_month = Month;

			DateTime dt = string.Format("{0}-{1}-1",_year,_month).ToDateTime();

			while (dt.Month == _month)
			{
				_days.Add(dt);
				dt = dt.AddDays(1);
			}

			dt = string.Format("{0}-{1}-1", _year, _month).ToDateTime();
			bool _continue=true;
			
			while (_continue)
			{
				YearWeek yw = dt.IsoWeek();
				if (yw.Days.Where(x => x.Month == _month).Count() > 0)
					_weeks.Add(yw);
				else
					_continue = false;

				dt = dt.AddDays(7);
			}

		}
	}

	/// <summary>
/// Represents the class public.
/// </summary>
public class PropertyComparer<T> : IEqualityComparer<T>
{
	private PropertyInfo _PropertyInfo;
	
	/// <summary>
	/// Creates a new instance of PropertyComparer.
	/// </summary>
	/// <param name="propertyName">The name of the property on type T 
	/// to perform the comparison on.</param>
	public PropertyComparer(string propertyName)
	{
		//store a reference to the property info object for use during the comparison
		_PropertyInfo = typeof(T).GetProperty(propertyName, 
	BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
		if (_PropertyInfo == null)
		{
			throw new ArgumentException(string.Format("{0} is not a property of type {1}.", propertyName, typeof(T)));
		}
	}
	
	#region IEqualityComparer<T> Members
	
	public bool Equals(T x, T y)
	{
		//get the current value of the comparison property of x and of y
		object xValue = _PropertyInfo.GetValue(x, null);
		object yValue = _PropertyInfo.GetValue(y, null);
		
		//if the xValue is null then we consider them equal if and only if yValue is null
		if (xValue == null)
			return yValue == null;
			
		//use the default comparer for whatever type the comparison property is.
		return xValue.Equals(yValue);
	}
	
	public int GetHashCode(T obj)
	{
		//get the value of the comparison property out of obj
		object propertyValue = _PropertyInfo.GetValue(obj, null);
		
		if (propertyValue == null)
			return 0;
			
		else
			return propertyValue.GetHashCode();
	}
	
	#endregion
}

    public static class Reflection
    {
        public static PropertyInfo GetProperty(this object t, string propertyName)
        {
            if (t.GetType().GetProperties().FirstOrDefault(p => p.Name == propertyName.Split('.')[0]) == null)
                throw new ArgumentNullException(string.Format("Property {0} does not exist in object {1}", propertyName, t.ToString()));
            if (!propertyName.Contains("."))
                return t.GetType().GetProperty(propertyName);
            else
                return GetProperty(t.GetType().GetProperty(propertyName.Split('.')[0]).GetValue(t, null), propertyName.Split('.')[1]);
        }
    }
}
