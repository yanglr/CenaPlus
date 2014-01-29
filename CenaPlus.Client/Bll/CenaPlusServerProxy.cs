﻿using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Net;
using CenaPlus.Entity;
using CenaPlus.Network;
namespace CenaPlus.Client.Bll
{
    public class CenaPlusServerProxy : ClientBase<ICenaPlusServer>, ICenaPlusServer
    {

        private static ServiceEndpoint GetServiceEndPoint(IPEndPoint server)
        {
            ServiceEndpoint endpoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(ICenaPlusServer)));
            Uri address = new UriBuilder("net.tcp", server.Address.ToString(), server.Port, "/CenaPlusServer").Uri;
            endpoint.Address = new EndpointAddress(address);
            endpoint.Binding = new NetTcpBinding(SecurityMode.None);
            return endpoint;
        }

        public CenaPlusServerProxy(IPEndPoint server, ICenaPlusServerCallback callback)
            : base(new InstanceContext(callback), GetServiceEndPoint(server))
        {
            // Check version
            var fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            string clientVersion = fileVersion.FileMajorPart + "." + fileVersion.FileMinorPart;
            string serverVersion = GetVersion();
            if (clientVersion != serverVersion)
            {
                throw new Exception(string.Format("Version of client({0}) does not match version of server({1}).", clientVersion, serverVersion));
            }
        }

        public string GetVersion()
        {
            return Channel.GetVersion();
        }

        public List<int> GetContestList()
        {
            return Channel.GetContestList();
        }

        public Contest GetContest(int id)
        {
            return Channel.GetContest(id);
        }

        public bool Authenticate(string userName, string password)
        {
            return Channel.Authenticate(userName, password);
        }

        public List<int> GetProblemList(int contestID)
        {
            return Channel.GetProblemList(contestID);
        }

        public Problem GetProblem(int id)
        {
            return Channel.GetProblem(id);
        }

        public int Submit(int problemID, string code, ProgrammingLanguage language)
        {
            return Channel.Submit(problemID, code, language);
        }

        public List<int> GetRecordList(int contestID)
        {
            return Channel.GetRecordList(contestID);
        }

        public Record GetRecord(int id)
        {
            return Channel.GetRecord(id);
        }

        public List<int> GetUserList()
        {
            return Channel.GetUserList();
        }

        public User GetUser(int id)
        {
            return Channel.GetUser(id);
        }

        public void UpdateUser(int id, string name, string nickname, string password, UserRole? role)
        {
            Channel.UpdateUser(id, name, nickname, password, role);
        }

        public void CreateUser(string name, string nickname, string password, UserRole role)
        {
            Channel.CreateUser(name, nickname, password, role);
        }

        public void DeleteUser(int id)
        {
            Channel.DeleteUser(id);
        }
    }
}
