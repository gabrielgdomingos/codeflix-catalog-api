using FC.CodeFlix.Catalog.Application.UseCases.Categories.ListCategories;
using System.Collections.Generic;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.ListCategories
{
    public class ListCategoriesUseCaseTestDataGenerator
    {
        public static IEnumerable<object[]> GetInputsWithoutAllParameters(int times = 12)
        {
            var fixture = new ListCategoriesUseCaseTestFixture();

            for (int i = 0; i < times; i++)
            {
                var input = fixture.GetValidInput();

                switch (i % 6)
                {
                    case 0:
                        yield return new object[] {
                            new ListCategoriesInput()
                        };
                        break;

                    case 1:
                        yield return new object[] {
                            new ListCategoriesInput(input.Page)
                        };
                        break;

                    case 2:
                        yield return new object[] {
                            new ListCategoriesInput(
                                input.Page,
                                input.PerPage
                            )
                        };
                        break;

                    case 3:
                        yield return new object[] {
                            new ListCategoriesInput(
                                input.Page,
                                input.PerPage,
                                input.Search
                            )
                        };
                        break;

                    case 4:
                        yield return new object[] {
                            new ListCategoriesInput(
                                input.Page,
                                input.PerPage,
                                input.Search,
                                input.Sort
                            )
                        };
                        break;

                    case 5:
                        yield return new object[] { input };
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
