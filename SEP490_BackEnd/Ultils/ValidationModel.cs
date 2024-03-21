using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SEP490_BackEnd.Ultils
{
    public class ValidationModel
    {
        public static Dictionary<string, string> GetValidationErrors(ModelStateDictionary modelState)
        {
            var validationErrors = new Dictionary<string, string>();

            //Get all errors
            foreach (var modelStateEntry in modelState)
            {
                var propertyName = modelStateEntry.Key;
                var errorMessages = modelStateEntry.Value.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                //Add to validation errors
                validationErrors.Add(propertyName.ToLower(), string.Join(", ", errorMessages));
            }

            return validationErrors;
        }
    }
}
