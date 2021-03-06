// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.Services
{
    /// <summary>
    /// Defines behavior for comparing properties of <see cref="OpenApiRequestBody"/>.
    /// </summary>
    public class OpenApiRequestBodyComparer : OpenApiComparerBase<OpenApiRequestBody>
    {
        /// <summary>
        /// Executes comparision against source and target <see cref="OpenApiRequestBody"/>.
        /// </summary>
        /// <param name="sourceRequestBody">The source.</param>
        /// <param name="targetRequestBody">The target.</param>
        /// <param name="comparisonContext">Context under which to compare the source and target.</param>
        public override void Compare(
            OpenApiRequestBody sourceRequestBody,
            OpenApiRequestBody targetRequestBody,
            ComparisonContext comparisonContext)
        {
            if (sourceRequestBody == null && targetRequestBody == null)
            {
                return;
            }

            if (sourceRequestBody == null || targetRequestBody == null)
            {
                comparisonContext.AddOpenApiDifference(
                    new OpenApiDifference
                    {
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        SourceValue = sourceRequestBody,
                        TargetValue = targetRequestBody,
                        OpenApiComparedElementType = typeof(OpenApiRequestBody),
                        Pointer = comparisonContext.PathString
                    });

                return;
            }

            new OpenApiReferenceComparer<OpenApiRequestBody>()
                .Compare(sourceRequestBody.Reference, targetRequestBody.Reference, comparisonContext);

            WalkAndCompare(comparisonContext, OpenApiConstants.Description,
                () => Compare(sourceRequestBody.Description, targetRequestBody.Description, comparisonContext));

            WalkAndCompare(comparisonContext, OpenApiConstants.Required,
                () => Compare(sourceRequestBody.Required, targetRequestBody.Required, comparisonContext));

            WalkAndCompare(
                comparisonContext,
                OpenApiConstants.Content,
                () => comparisonContext
                    .GetComparer<IDictionary<string, OpenApiMediaType>>()
                    .Compare(sourceRequestBody.Content, targetRequestBody.Content, comparisonContext));
        }
    }
}
