using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class CheckinToolDummies
    {
        private static IList<Employee> _employees;
        private static IList<Company> _companys;

        public static CheckinTool CreateOneCheckinTool()
        {
            return new CheckinTool
            {
                Id = 52,
                CheckinDateTime = new DateTime(2014, 1, 21, 13, 23, 00),
                EmployeeCompanyHolderId = 1,
                Tool = new Tool {Id = 1, Name = "Ferramenta 1"},
                CompanyAreaId = 1
            };
        }

        public static CheckinTool CreateOneCheckinToolWithoutCompanyArea()
        {
            return new CheckinTool
            {
                Id = 52,
                CheckinDateTime = new DateTime(2014, 1, 21, 13, 23, 00),
                EmployeeCompanyHolderId = 1,
                Tool = new Tool { Id = 1, Name = "Ferramenta 1" }
            };
        }

        public static IList<Holder> CreateHoldersForCheckins()
        {
            #region Create Companys and Employees Dummies
            _employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Celso",
                    LastName = "Gay",
                    IsDeleted = false
                },
                new Employee
                {
                    Id = 2,
                    Name = "Brendon",
                    LastName = "Gay",
                    IsDeleted = false
                },
                new Employee
                {
                    Id = 3,
                    Name = "Lorena",
                    LastName = "Gay",
                    IsDeleted = false
                }
            };

            _companys = new List<Company>
            {
                new Company
                {
                    Id = 4,
                    Name = "Portoverano",
                    Email = "adm@portoverano.com",
                    CompanyAreas = new Collection<CompanyArea>
                    {
                        new CompanyArea
                        {
                            Id = 2,
                            Name = "Portão de visitantes"
                        },
                        new CompanyArea
                        {
                            Id = 1,
                            Name = "Portão"
                        }
                    }
                },
                new Company
                {
                    Id = 5,
                    Name = "Portofino",
                    Email = "adm@portofino.com",
                    CompanyAreas = new Collection<CompanyArea>
                    {
                        new CompanyArea
                        {
                            Id = 1,
                            Name = "Portão"
                        }
                    }
                },
                new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com",
                    CompanyAreas = new Collection<CompanyArea>
                    {
                        new CompanyArea
                        {
                            Id = 2,
                            Name = "Portão de visitantes"
                        }
                    }
                }
            };
            #endregion

            var holders = _companys.Cast<Holder>().ToList();
            holders.AddRange(_employees);

            return holders;
        }

        public static IList<CheckinTool> CreateCheckinTools()
        {
            CreateHoldersForCheckins();

            return new List<CheckinTool>
            {
                new CheckinTool
                {
                    Id = 1,
                    CheckinDateTime = new DateTime(2013, 12, 09, 12, 32, 00), 
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Celso").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 1",
                        Id = 1
                    }
                },
                new CheckinTool
                {
                    Id = 3,
                    CheckinDateTime = new DateTime(2013, 12, 10, 14, 22, 00),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Brendon").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 1",
                        Id = 1
                    }
                },
                new CheckinTool
                {
                    Id = 30,
                    CheckinDateTime = new DateTime(2013, 12, 09, 12, 32, 00),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Brendon").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 2,
                    CheckinDateTime = new DateTime(2013, 12, 10, 12, 32, 00),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Lorena").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 4,
                    CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portomare").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    },
                    CompanyAreaId = 1
                },
                new CheckinTool
                {
                    Id = 6,
                    CheckinDateTime = new DateTime(2013, 12, 10, 17, 32, 00),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Celso").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 7,
                    CheckinDateTime = new DateTime(2013, 12, 11, 11, 02, 00),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portomare").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 40,
                    CheckinDateTime = new DateTime(2013, 12, 12, 11, 02, 00),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Celso").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 41,
                    CheckinDateTime = new DateTime(2013, 12, 17, 11, 02, 00),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portoverano").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 42,
                    CheckinDateTime = new DateTime(2013, 12, 21, 11, 02, 00),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portomare").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 5,
                    CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Brendon").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 3",
                        Id = 3
                    }
                },
                new CheckinTool
                {
                    Id = 8,
                    CheckinDateTime = new DateTime(2013, 12, 11, 12, 32, 00),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portoverano").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 3",
                        Id = 3
                    }
                },
                new CheckinTool
                {
                    Id = 33,
                    CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Brendon").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 4",
                        Id = 4
                    }
                }
            };
        }

        public static CheckinToolViewModel CreateOneCheckinToolViewModel()
        {
            return new CheckinToolViewModel
            {
                Id = 1,
                CheckinDateTime = new DateTime(2014, 1, 21, 13, 23, 00),
                EmployeeCompanyHolderName = "Celso",
                ToolName = "Ferramenta 1",
                CompanyAreaName = "Portão"
            };
        }

        public static CheckinToolViewModel CreateOneCheckinToolViewModelWithoutCompanyArea()
        {
            return new CheckinToolViewModel
            {
                Id = 1,
                CheckinDateTime = new DateTime(2014, 1, 21, 13, 23, 00),
                EmployeeCompanyHolderName = "Celso",
                ToolName = "Ferramenta 1"
            };
        }

        public static CheckinToolTabViewModel CreateOneCheckinToolTabViewModel(int toolId)
        {
            return new CheckinToolTabViewModel
            {
                CheckinDateTime = new DateTime(2014, 1, 21, 13, 23, 00),
                HolderName = "Celso",
                ToolId = toolId,
                CompanyAreaName = "Portão"
            };
        }

        public static CheckinToolTabViewModel CreateOneCheckinToolTabViewModelWithoutCompanyArea(int toolId)
        {
            return new CheckinToolTabViewModel
            {
                CheckinDateTime = new DateTime(2014, 1, 21, 13, 23, 00),
                HolderName = "Celso",
                ToolId = toolId,
            };
        }

    }
}
