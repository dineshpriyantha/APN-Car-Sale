// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace APN_Car_Sale.DependencyResolution
{
    using APNCarSaleDataService.Interfaces;
    using APNCarSaleDataService.Models;
    using APNCarSaleDataService.Repositories;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            //For<IExample>().Use<Example>();
            For<IRepository<APN_User, int>>().Use<UserRepository>();
            For<IRepository<APN_Vehicle, int>>().Use<VehicleRepository>();
            For<IRepository<APN_Category, int>>().Use<CategoryRepository>();
            For<IRepository<APN_UserMaster, int>>().Use<UserMasterRepository>();
            For<IRepository<APN_SubCategory, int>>().Use<SubCategoryRepository>();
        }

        #endregion
    }
}