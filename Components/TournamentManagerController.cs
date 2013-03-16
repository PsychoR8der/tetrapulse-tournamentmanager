// 
// DotNetNuke® - http://www.dotnetnuke.com 
// Copyright (c) 2002-2013 
// by DotNetNuke Corporation 
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software. 
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE. 
// 

using System;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Web;
using System.Collections.Generic;

using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Search;
using DotNetNuke.Entities.Modules;

namespace TetraPulse.Modules.TournamentManager
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// The Controller class for TournamentManager 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class TournamentManagerController : ISearchable, IPortable
    {

        #region "Public Methods"

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="ModuleId">The Id of the module</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public List<TournamentManagerInfo> GetTournamentManagers(int ModuleId)
        {

            return CBO.FillCollection<TournamentManagerInfo>(DataProvider.Instance().GetTournamentManagers(ModuleId));

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="ModuleId">The Id of the module</param> 
        /// <param name="ItemId">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public TournamentManagerInfo GetTournamentManager(int ModuleId, int ItemId)
        {

            return (TournamentManagerInfo)CBO.FillObject(DataProvider.Instance().GetTournamentManager(ModuleId, ItemId), typeof(TournamentManagerInfo));

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// adds an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objTournamentManager">The TournamentManagerInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void AddTournamentManager(TournamentManagerInfo objTournamentManager)
        {

            if (objTournamentManager.Content.Trim() != "")
            {
                DataProvider.Instance().AddTournamentManager(objTournamentManager.ModuleId, objTournamentManager.Content, objTournamentManager.CreatedByUser);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// saves an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objTournamentManager">The TournamentManagerInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UpdateTournamentManager(TournamentManagerInfo objTournamentManager)
        {

            if (objTournamentManager.Content.Trim() != "")
            {
                DataProvider.Instance().UpdateTournamentManager(objTournamentManager.ModuleId, objTournamentManager.ItemId, objTournamentManager.Content, objTournamentManager.CreatedByUser);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="ModuleId">The Id of the module</param> 
        /// <param name="ItemId">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void DeleteTournamentManager(int ModuleId, int ItemId)
        {

            DataProvider.Instance().DeleteTournamentManager(ModuleId, ItemId);

        }

        #endregion

        #region "Optional Interfaces"

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// GetSearchItems implements the ISearchable Interface 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(ModuleInfo ModInfo)
        {

            SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

            List<TournamentManagerInfo> colTournamentManagers = GetTournamentManagers(ModInfo.ModuleID);
            foreach (TournamentManagerInfo objTournamentManager in colTournamentManagers)
            {
                SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objTournamentManager.Content, objTournamentManager.CreatedByUser, objTournamentManager.CreatedDate, ModInfo.ModuleID, objTournamentManager.ItemId.ToString(), objTournamentManager.Content, "ItemId=" + objTournamentManager.ItemId.ToString());
                SearchItemCollection.Add(SearchItem);
            }

            return SearchItemCollection;

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// ExportModule implements the IPortable ExportModule Interface 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="ModuleID">The Id of the module to be exported</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public string ExportModule(int ModuleID)
        {

            string strXML = "";

            List<TournamentManagerInfo> colTournamentManagers = GetTournamentManagers(ModuleID);
            if (colTournamentManagers.Count != 0)
            {
                strXML += "<TournamentManagers>";
                foreach (TournamentManagerInfo objTournamentManager in colTournamentManagers)
                {
                    strXML += "<TournamentManager>";
                    strXML += "<content>" + XmlUtils.XMLEncode(objTournamentManager.Content) + "</content>";
                    strXML += "</TournamentManager>";
                }
                strXML += "</TournamentManagers>";
            }

            return strXML;

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// ImportModule implements the IPortable ImportModule Interface 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="ModuleID">The Id of the module to be imported</param> 
        /// <param name="Content">The content to be imported</param> 
        /// <param name="Version">The version of the module to be imported</param> 
        /// <param name="UserId">The Id of the user performing the import</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void ImportModule(int ModuleID, string Content, string Version, int UserId)
        {

            XmlNode xmlTournamentManagers = Globals.GetContent(Content, "TournamentManagers");
            foreach (XmlNode xmlTournamentManager in xmlTournamentManagers.SelectNodes("TournamentManager"))
            {
                TournamentManagerInfo objTournamentManager = new TournamentManagerInfo();
                objTournamentManager.ModuleId = ModuleID;
                objTournamentManager.Content = xmlTournamentManager.SelectSingleNode("content").InnerText;
                objTournamentManager.CreatedByUser = UserId;
                AddTournamentManager(objTournamentManager);
            }

        }

        #endregion

    }
}