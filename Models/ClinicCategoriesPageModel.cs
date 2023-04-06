using Microsoft.AspNetCore.Mvc.RazorPages;
using BloodNetwork.Data;
using BloodNetwork.Models;

namespace BloodNetwork.Models
{
    public class ClinicCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(BloodNetworkContext context, Clinic clinic)
        {
            var allCategories = context.Category;
            var clinicCategories = new HashSet<int>(clinic.ClinicCategories.Select(c => c.CategoryID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = clinicCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateClinicCategories(BloodNetworkContext context,
        string[] selectedCategories, Clinic clinicToUpdate)
        {
            if (selectedCategories == null)
            {
                clinicToUpdate.ClinicCategories = new List<ClinicCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var clinicCategories = new HashSet<int>
            (clinicToUpdate.ClinicCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!clinicCategories.Contains(cat.ID))
                    {
                        clinicToUpdate.ClinicCategories.Add(
                        new ClinicCategory
                        {
                            ClinicID = clinicToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (clinicCategories.Contains(cat.ID))
                    {
                       ClinicCategory courseToRemove
                        = clinicToUpdate
                        .ClinicCategories
                        .SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}

