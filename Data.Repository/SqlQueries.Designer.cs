﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Farfetch.Buildionaire.Data.Repository {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SqlQueries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SqlQueries() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Farfetch.Buildionaire.Data.Repository.SqlQueries", typeof(SqlQueries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ;with hrs as ( 	select 0 h	union all 	select h+1  from hrs where h &lt; 23 ),
        ///tmp as ( 
        ///select hrs.h, convert(decimal,coalesce(Sum(cnt),0)) cnt from hrs  left join  (select datepart(YEAR, CreatedAt) y, datepart(Month, CreatedAt) m, datepart(day, CreatedAt) d, datepart(hour, CreatedAt) h, count(1) cnt from ChangeSets group by datepart(YEAR, CreatedAt), datepart(Month, CreatedAt), datepart(day, CreatedAt), datepart(hour, CreatedAt)) a on hrs.h = a.h group by hrs.h
        ///)
        ///select convert(varchar(3),h) Item1, conver [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string checkinsPerHourScript {
            get {
                return ResourceManager.GetString("checkinsPerHourScript", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ;with tmp as
        ///(
        ///	select DATEADD(month, DATEDIFF(month, 0, &apos;2014-01-01&apos;), 0) dt, 0 sub
        ///	union all
        ///	select DATEADD(month, +1, dt), 	sub+1
        ///	from tmp
        ///	where dt &lt; getdate()
        ///),
        ///dts as 
        ///(
        ///	select a.dt InitDate, b.dt EndDate
        ///	from tmp a 
        ///		inner join tmp b on a.sub = b.sub-1
        ///)
        ///,
        ///bld as 
        ///(
        ///	select b.id, b.Project_Id, b.CreatedAt, d.InitDate, b.TotalScore	
        ///	from dts d
        ///		inner join builds b on b.CreatedAt between D.InitDate and D.EndDate
        ///)
        ///select convert(varchar(10),d.InitDate,111) Item1, cast(coal [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetMonthTotalScore {
            get {
                return ResourceManager.GetString("GetMonthTotalScore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ;with tmp as
        ///(
        ///	select DATEADD(month, DATEDIFF(month, 0, &apos;2014-01-01&apos;), 0) dt, 0 sub
        ///	union all
        ///	select DATEADD(month, +1, dt), 	sub+1
        ///	from tmp
        ///	where dt &lt; getdate()
        ///),
        ///dts as 
        ///(
        ///	select a.dt InitDate, b.dt EndDate
        ///	from tmp a 
        ///		inner join tmp b on a.sub = b.sub-1
        ///)
        ///,
        ///bld as 
        ///(
        ///	select b.id, b.Project_Id, b.CreatedAt, d.InitDate, b.TotalScore	
        ///	from dts d
        ///		inner join builds b 
        ///			inner join DashboardProjects dp on b.Project_Id = dp.Project_Id
        ///			inner join Dashboards da on dp.Dashboa [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetMonthTotalScoreFiltered {
            get {
                return ResourceManager.GetString("GetMonthTotalScoreFiltered", resourceCulture);
            }
        }
    }
}