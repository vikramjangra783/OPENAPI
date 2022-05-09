﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. 

using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Services;

namespace Microsoft.OpenApi.Tests.Services
{
    internal static class OpenApiComparerTestCases
    {
        public static IEnumerable<object[]> GetTestCasesForOpenApiComparerShouldSucceed()
        {
            // New and removed paths
            yield return new object[]
            {
                "New And Removed Paths",
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/newPath", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new List<OpenApiDifference>
                {
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1newPath",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiPathItem),
                        SourceValue = null,
                        TargetValue = new OpenApiPathItem
                        {
                            Summary = "test",
                            Description = "test",
                            Operations = new Dictionary<OperationType, OpenApiOperation>
                            {
                                {
                                    OperationType.Get, new OpenApiOperation()
                                }
                            }
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiPathItem),
                        TargetValue = null,
                        SourceValue = new OpenApiPathItem
                        {
                            Summary = "test",
                            Description = "test",
                            Operations = new Dictionary<OperationType, OpenApiOperation>
                            {
                                {
                                    OperationType.Get, new OpenApiOperation()
                                }
                            }
                        }
                    }
                }
            };

            // New and removed operations
            yield return new object[]
            {
                "New And Removed Operations",
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    },
                                    {
                                        OperationType.Post, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    },
                                    {
                                        OperationType.Patch, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new List<OpenApiDifference>
                {
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/patch",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiOperation),
                        SourceValue = null,
                        TargetValue = new OpenApiOperation()
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/post",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiOperation),
                        TargetValue = null,
                        SourceValue = new OpenApiOperation()
                    }
                }
            };

            // Empty target document paths
            yield return new object[]
            {
                "Empty target document",
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new OpenApiDocument(),
                new List<OpenApiDifference>
                {
                    new OpenApiDifference
                    {
                        Pointer = "#/paths",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(OpenApiPaths),
                        SourceValue = new OpenApiPaths
                        {
                            {
                                "/test", new OpenApiPathItem
                                {
                                    Summary = "test",
                                    Description = "test",
                                    Operations = new Dictionary<OperationType, OpenApiOperation>
                                    {
                                        {
                                            OperationType.Get, new OpenApiOperation()
                                        }
                                    }
                                }
                            }
                        },
                        TargetValue = null
                    }
                }
            };

            // Empty source document
            yield return new object[]
            {
                "Empty source document",
                new OpenApiDocument(),
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/newPath", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new List<OpenApiDifference>
                {
                    new OpenApiDifference
                    {
                        Pointer = "#/paths",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(OpenApiPaths),
                        SourceValue = null,
                        TargetValue = new OpenApiPaths
                        {
                            {
                                "/newPath", new OpenApiPathItem
                                {
                                    Summary = "test",
                                    Description = "test",
                                    Operations = new Dictionary<OperationType, OpenApiOperation>
                                    {
                                        {
                                            OperationType.Get, new OpenApiOperation()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Empty target operations
            yield return new object[]
            {
                "Empty target operations",
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    },
                                    {
                                        OperationType.Post, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>()
                            }
                        }
                    }
                },
                new List<OpenApiDifference>
                {
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiOperation),
                        TargetValue = null,
                        SourceValue = new OpenApiOperation()
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/post",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiOperation),
                        TargetValue = null,
                        SourceValue = new OpenApiOperation()
                    }
                }
            };

            // Empty source operations
            yield return new object[]
            {
                "Empty source operations",
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>()
                            }
                        }
                    }
                },
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    },
                                    {
                                        OperationType.Patch, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new List<OpenApiDifference>
                {
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiOperation),
                        SourceValue = null,
                        TargetValue = new OpenApiOperation()
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/patch",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiOperation),
                        SourceValue = null,
                        TargetValue = new OpenApiOperation()
                    }
                }
            };

            // Identical source and target
            yield return new object[]
            {
                "Identical source and target documents",
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    },
                                    {
                                        OperationType.Post, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    },
                                    {
                                        OperationType.Post, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new List<OpenApiDifference>()
            };

            // Differences in summary and description
            yield return new object[]
            {
                "Differences in summary and description",
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    },
                                    {
                                        OperationType.Post, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "updated",
                                Description = "updated",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation()
                                    },
                                    {
                                        OperationType.Post, new OpenApiOperation()
                                    }
                                }
                            }
                        }
                    }
                },
                new List<OpenApiDifference>
                {
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/summary",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(string),
                        SourceValue = "test",
                        TargetValue = "updated"
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/description",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(string),
                        SourceValue = "test",
                        TargetValue = "updated"
                    }
                }
            };

            // Differences in schema
            yield return new object[]
            {
                "Differences in schema",
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation
                                        {
                                            Parameters = new List<OpenApiParameter>
                                            {
                                                new OpenApiParameter
                                                {
                                                    Name = "Test Parameter",
                                                    In = ParameterLocation.Path,
                                                    Schema = new OpenApiSchema
                                                    {
                                                        Title = "title1",
                                                        MultipleOf = 3,
                                                        Maximum = 42,
                                                        ExclusiveMinimum = true,
                                                        Minimum = 10,
                                                        Default = new OpenApiInteger(15),
                                                        Type = "integer",

                                                        Nullable = true,
                                                        ExternalDocs = new OpenApiExternalDocs
                                                        {
                                                            Url = new Uri("http://example.com/externalDocs")
                                                        },

                                                        Reference = new OpenApiReference
                                                        {
                                                            Type = ReferenceType.Schema,
                                                            Id = "schemaObject1"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Components = new OpenApiComponents
                    {
                        Schemas = new Dictionary<string, OpenApiSchema>
                        {
                            ["schemaObject1"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property7"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject2"
                                        }
                                    }
                                },
                                Example = new OpenApiObject
                                {
                                    ["status"] = new OpenApiString("Status1"),
                                    ["id"] = new OpenApiString("v1"),
                                    ["links"] = new OpenApiArray
                                    {
                                        new OpenApiObject
                                        {
                                            ["href"] = new OpenApiString("http://example.com/1"),
                                            ["rel"] = new OpenApiString("sampleRel1")
                                        }
                                    }
                                }
                            },
                            ["schemaObject2"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property5"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject1"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation
                                        {
                                            Parameters = new List<OpenApiParameter>
                                            {
                                                new OpenApiParameter
                                                {
                                                    Name = "Test Parameter",
                                                    In = ParameterLocation.Path,
                                                    Schema = new OpenApiSchema
                                                    {
                                                        Title = "title1",
                                                        MultipleOf = 3,
                                                        Maximum = 42,
                                                        ExclusiveMinimum = true,
                                                        Minimum = 10,
                                                        Default = new OpenApiInteger(15),
                                                        Type = "integer",

                                                        Nullable = true,
                                                        ExternalDocs = new OpenApiExternalDocs
                                                        {
                                                            Url = new Uri("http://example.com/externalDocs")
                                                        },

                                                        Reference = new OpenApiReference
                                                        {
                                                            Type = ReferenceType.Schema,
                                                            Id = "schemaObject1"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Components = new OpenApiComponents
                    {
                        Schemas = new Dictionary<string, OpenApiSchema>
                        {
                            ["schemaObject1"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property5"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject2"
                                        }
                                    }
                                },
                                Example = new OpenApiObject
                                {
                                    ["status"] = new OpenApiString("Status1"),
                                    ["id"] = new OpenApiString("v1"),
                                    ["links"] = new OpenApiArray
                                    {
                                        new OpenApiObject
                                        {
                                            ["href"] = new OpenApiString("http://example.com/1"),
                                            ["relupdate"] = new OpenApiString("sampleRel1")
                                        }
                                    }
                                }
                            },
                            ["schemaObject2"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property5"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject1"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new List<OpenApiDifference>
                {
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get/parameters/0/schema/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get/parameters/0/schema/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/parameters/0/schema/properties/property6/properties/property6/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/parameters/0/schema/properties/property6/properties/property6/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/components/schemas/schemaObject1/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/components/schemas/schemaObject1/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/schemas/schemaObject1/properties/property6/properties/property6/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/schemas/schemaObject1/properties/property6/properties/property6/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/schemas/schemaObject2/properties/property6/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/schemas/schemaObject2/properties/property6/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/components/schemas/schemaObject1/example",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(IOpenApiAny),
                        SourceValue = new OpenApiObject
                        {
                            ["status"] = new OpenApiString("Status1"),
                            ["id"] = new OpenApiString("v1"),
                            ["links"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["href"] = new OpenApiString("http://example.com/1"),
                                    ["rel"] = new OpenApiString("sampleRel1")
                                }
                            }
                        },
                        TargetValue = new OpenApiObject
                        {
                            ["status"] = new OpenApiString("Status1"),
                            ["id"] = new OpenApiString("v1"),
                            ["links"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["href"] = new OpenApiString("http://example.com/1"),
                                    ["relupdate"] = new OpenApiString("sampleRel1")
                                }
                            }
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/components/schemas/schemaObject2/properties/property6/example",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(IOpenApiAny),
                        SourceValue = new OpenApiObject
                        {
                            ["status"] = new OpenApiString("Status1"),
                            ["id"] = new OpenApiString("v1"),
                            ["links"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["href"] = new OpenApiString("http://example.com/1"),
                                    ["rel"] = new OpenApiString("sampleRel1")
                                }
                            }
                        },
                        TargetValue = new OpenApiObject
                        {
                            ["status"] = new OpenApiString("Status1"),
                            ["id"] = new OpenApiString("v1"),
                            ["links"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["href"] = new OpenApiString("http://example.com/1"),
                                    ["relupdate"] = new OpenApiString("sampleRel1")
                                }
                            }
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/parameters/0/schema/properties/property6/properties/property6/example",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(IOpenApiAny),
                        SourceValue = new OpenApiObject
                        {
                            ["status"] = new OpenApiString("Status1"),
                            ["id"] = new OpenApiString("v1"),
                            ["links"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["href"] = new OpenApiString("http://example.com/1"),
                                    ["rel"] = new OpenApiString("sampleRel1")
                                }
                            }
                        },
                        TargetValue = new OpenApiObject
                        {
                            ["status"] = new OpenApiString("Status1"),
                            ["id"] = new OpenApiString("v1"),
                            ["links"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["href"] = new OpenApiString("http://example.com/1"),
                                    ["relupdate"] = new OpenApiString("sampleRel1")
                                }
                            }
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get/parameters/0/schema/example",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(IOpenApiAny),
                        SourceValue = new OpenApiObject
                        {
                            ["status"] = new OpenApiString("Status1"),
                            ["id"] = new OpenApiString("v1"),
                            ["links"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["href"] = new OpenApiString("http://example.com/1"),
                                    ["rel"] = new OpenApiString("sampleRel1")
                                }
                            }
                        },
                        TargetValue = new OpenApiObject
                        {
                            ["status"] = new OpenApiString("Status1"),
                            ["id"] = new OpenApiString("v1"),
                            ["links"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["href"] = new OpenApiString("http://example.com/1"),
                                    ["relupdate"] = new OpenApiString("sampleRel1")
                                }
                            }
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/schemas/schemaObject1/properties/property6/properties/property6/example",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(IOpenApiAny),
                        SourceValue = new OpenApiObject
                        {
                            ["status"] = new OpenApiString("Status1"),
                            ["id"] = new OpenApiString("v1"),
                            ["links"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["href"] = new OpenApiString("http://example.com/1"),
                                    ["rel"] = new OpenApiString("sampleRel1")
                                }
                            }
                        },
                        TargetValue = new OpenApiObject
                        {
                            ["status"] = new OpenApiString("Status1"),
                            ["id"] = new OpenApiString("v1"),
                            ["links"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["href"] = new OpenApiString("http://example.com/1"),
                                    ["relupdate"] = new OpenApiString("sampleRel1")
                                }
                            }
                        }
                    }
                }
            };

            // Differences in request and response
            yield return new object[]
            {
                "Differences in request and response",
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation
                                        {
                                            RequestBody = new OpenApiRequestBody
                                            {
                                                Description = "description",
                                                Required = true,
                                                Content =
                                                {
                                                    ["application/xml"] = new OpenApiMediaType
                                                    {
                                                        Schema = new OpenApiSchema
                                                        {
                                                            Reference = new OpenApiReference
                                                            {
                                                                Id = "schemaObject1",
                                                                Type = ReferenceType.Schema
                                                            }
                                                        },
                                                        Examples = new Dictionary<string, OpenApiExample>
                                                        {
                                                            {
                                                                "example1", new OpenApiExample
                                                                {
                                                                    Reference = new OpenApiReference
                                                                    {
                                                                        Id = "example1",
                                                                        Type = ReferenceType.Example
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            Responses = new OpenApiResponses
                                            {
                                                {
                                                    "200",
                                                    new OpenApiResponse
                                                    {
                                                        Description = "An updated complex object array response",
                                                        Content =
                                                        {
                                                            ["application/json"] = new OpenApiMediaType
                                                            {
                                                                Schema = new OpenApiSchema
                                                                {
                                                                    Type = "array",
                                                                    Items = new OpenApiSchema
                                                                    {
                                                                        Reference = new OpenApiReference
                                                                        {
                                                                            Type = ReferenceType.Schema,
                                                                            Id = "schemaObject1"
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Components = new OpenApiComponents
                    {
                        Schemas = new Dictionary<string, OpenApiSchema>
                        {
                            ["schemaObject1"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property7"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject2"
                                        }
                                    }
                                }
                            },
                            ["schemaObject2"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property5"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject1"
                                        }
                                    }
                                }
                            }
                        },
                        Examples = new Dictionary<string, OpenApiExample>
                        {
                            ["example1"] = new OpenApiExample
                            {
                                Value = new OpenApiObject
                                {
                                    ["versions"] = new OpenApiArray
                                    {
                                        new OpenApiObject
                                        {
                                            ["status"] = new OpenApiString("Status1"),
                                            ["id"] = new OpenApiString("v1"),
                                            ["links"] = new OpenApiArray
                                            {
                                                new OpenApiObject
                                                {
                                                    ["href"] = new OpenApiString("http://example.com/1"),
                                                    ["rel"] = new OpenApiString("sampleRel1")
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            ["example3"] = new OpenApiExample
                            {
                                Value = new OpenApiObject
                                {
                                    ["versions"] = new OpenApiObject
                                    {
                                        ["status"] = new OpenApiString("Status1"),
                                        ["id"] = new OpenApiString("v1"),
                                        ["links"] = new OpenApiArray
                                        {
                                            new OpenApiObject
                                            {
                                                ["href"] = new OpenApiString("http://example.com/1"),
                                                ["rel"] = new OpenApiString("sampleRel1")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation
                                        {
                                            RequestBody = new OpenApiRequestBody
                                            {
                                                Description = "description",
                                                Required = true,
                                                Content =
                                                {
                                                    ["application/xml"] = new OpenApiMediaType
                                                    {
                                                        Schema = new OpenApiSchema
                                                        {
                                                            Reference = new OpenApiReference
                                                            {
                                                                Id = "schemaObject1",
                                                                Type = ReferenceType.Schema
                                                            }
                                                        },
                                                        Examples = new Dictionary<string, OpenApiExample>
                                                        {
                                                            {
                                                                "example1", new OpenApiExample
                                                                {
                                                                    Reference = new OpenApiReference
                                                                    {
                                                                        Id = "example1",
                                                                        Type = ReferenceType.Example
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            Responses = new OpenApiResponses
                                            {
                                                {
                                                    "200",
                                                    new OpenApiResponse
                                                    {
                                                        Description = "An updated complex object array response",
                                                        Content =
                                                        {
                                                            ["application/json"] = new OpenApiMediaType
                                                            {
                                                                Schema = new OpenApiSchema
                                                                {
                                                                    Type = "array",
                                                                    Items = new OpenApiSchema
                                                                    {
                                                                        Reference = new OpenApiReference
                                                                        {
                                                                            Type = ReferenceType.Schema,
                                                                            Id = "schemaObject1"
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                },
                                                {
                                                    "400",
                                                    new OpenApiResponse
                                                    {
                                                        Description = "An updated complex object array response",
                                                        Content =
                                                        {
                                                            ["application/json"] = new OpenApiMediaType
                                                            {
                                                                Schema = new OpenApiSchema
                                                                {
                                                                    Type = "string"
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Components = new OpenApiComponents
                    {
                        Schemas = new Dictionary<string, OpenApiSchema>
                        {
                            ["schemaObject1"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property5"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject2"
                                        }
                                    }
                                }
                            },
                            ["schemaObject2"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property5"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject1"
                                        }
                                    }
                                }
                            }
                        },
                        Examples = new Dictionary<string, OpenApiExample>
                        {
                            ["example1"] = new OpenApiExample
                            {
                                Value = new OpenApiObject
                                {
                                    ["versions"] = new OpenApiArray
                                    {
                                        new OpenApiObject
                                        {
                                            ["status"] = new OpenApiString("Status1"),
                                            ["id"] = new OpenApiString("v1"),
                                            ["links"] = new OpenApiArray
                                            {
                                                new OpenApiObject
                                                {
                                                    ["href"] = new OpenApiString("http://example.com/1"),
                                                    ["relupdate"] = new OpenApiString("sampleRel1")
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            ["example3"] = new OpenApiExample
                            {
                                Value = new OpenApiObject
                                {
                                    ["versions"] = new OpenApiObject
                                    {
                                        ["status"] = new OpenApiString("Status1"),
                                        ["id"] = new OpenApiString("v1"),
                                        ["links"] = new OpenApiArray
                                        {
                                            new OpenApiObject
                                            {
                                                ["href"] = new OpenApiString("http://example.com/1"),
                                                ["rel"] = new OpenApiString("sampleRel1")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new List<OpenApiDifference>
                {
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get/requestBody/content/application~1xml/schema/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get/requestBody/content/application~1xml/schema/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/requestBody/content/application~1xml/schema/properties/property6/properties/property6/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/requestBody/content/application~1xml/schema/properties/property6/properties/property6/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get/responses/400",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiResponse),
                        SourceValue = null,
                        TargetValue = new OpenApiResponse
                        {
                            Description = "An updated complex object array response",
                            Content =
                            {
                                ["application/json"] = new OpenApiMediaType
                                {
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "string"
                                    }
                                }
                            }
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/responses/200/content/application~1json/schema/items/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/responses/200/content/application~1json/schema/items/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/responses/200/content/application~1json/schema/items/properties/property6/properties/property6/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/responses/200/content/application~1json/schema/items/properties/property6/properties/property6/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/components/schemas/schemaObject1/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/components/schemas/schemaObject1/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/schemas/schemaObject1/properties/property6/properties/property6/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/schemas/schemaObject1/properties/property6/properties/property6/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/schemas/schemaObject2/properties/property6/properties/property5",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = null,
                        TargetValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/schemas/schemaObject2/properties/property6/properties/property7",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSchema),
                        SourceValue = new OpenApiSchema
                        {
                            Type = "string",
                            MaxLength = 15
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get/requestBody/content/application~1xml/examples/example1/value",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(IOpenApiAny),
                        SourceValue = new OpenApiObject
                        {
                            ["versions"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["status"] = new OpenApiString("Status1"),
                                    ["id"] = new OpenApiString("v1"),
                                    ["links"] = new OpenApiArray
                                    {
                                        new OpenApiObject
                                        {
                                            ["href"] = new OpenApiString("http://example.com/1"),
                                            ["rel"] = new OpenApiString("sampleRel1")
                                        }
                                    }
                                }
                            }
                        },
                        TargetValue = new OpenApiObject
                        {
                            ["versions"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["status"] = new OpenApiString("Status1"),
                                    ["id"] = new OpenApiString("v1"),
                                    ["links"] = new OpenApiArray
                                    {
                                        new OpenApiObject
                                        {
                                            ["href"] = new OpenApiString("http://example.com/1"),
                                            ["relupdate"] = new OpenApiString("sampleRel1")
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/components/examples/example1/value",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(IOpenApiAny),
                        SourceValue = new OpenApiObject
                        {
                            ["versions"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["status"] = new OpenApiString("Status1"),
                                    ["id"] = new OpenApiString("v1"),
                                    ["links"] = new OpenApiArray
                                    {
                                        new OpenApiObject
                                        {
                                            ["href"] = new OpenApiString("http://example.com/1"),
                                            ["rel"] = new OpenApiString("sampleRel1")
                                        }
                                    }
                                }
                            }
                        },
                        TargetValue = new OpenApiObject
                        {
                            ["versions"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["status"] = new OpenApiString("Status1"),
                                    ["id"] = new OpenApiString("v1"),
                                    ["links"] = new OpenApiArray
                                    {
                                        new OpenApiObject
                                        {
                                            ["href"] = new OpenApiString("http://example.com/1"),
                                            ["relupdate"] = new OpenApiString("sampleRel1")
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Differences in tags and security requirements
            yield return new object[]
            {
                "Differences in tags and security requirements",
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation
                                        {
                                            RequestBody = new OpenApiRequestBody
                                            {
                                                Description = "description",
                                                Required = true,
                                                Content =
                                                {
                                                    ["application/xml"] = new OpenApiMediaType
                                                    {
                                                        Schema = new OpenApiSchema
                                                        {
                                                            Reference = new OpenApiReference
                                                            {
                                                                Id = "schemaObject1",
                                                                Type = ReferenceType.Schema
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            Responses = new OpenApiResponses
                                            {
                                                {
                                                    "200",
                                                    new OpenApiResponse
                                                    {
                                                        Description = "An updated complex object array response",
                                                        Content =
                                                        {
                                                            ["application/json"] = new OpenApiMediaType
                                                            {
                                                                Schema = new OpenApiSchema
                                                                {
                                                                    Type = "array",
                                                                    Items = new OpenApiSchema
                                                                    {
                                                                        Reference = new OpenApiReference
                                                                        {
                                                                            Type = ReferenceType.Schema,
                                                                            Id = "schemaObject1"
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            Security = new List<OpenApiSecurityRequirement>
                                            {
                                                new OpenApiSecurityRequirement
                                                {
                                                    [
                                                        new OpenApiSecurityScheme
                                                        {
                                                            Reference = new OpenApiReference
                                                            {
                                                                Type = ReferenceType.SecurityScheme,
                                                                Id = "scheme1"
                                                            }
                                                        }
                                                    ] = new List<string>()
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Tags = new List<OpenApiTag>
                    {
                        new OpenApiTag
                        {
                            Description = "test description",
                            Name = "Tag1",
                            ExternalDocs = new OpenApiExternalDocs
                            {
                                Description = "test description",
                                Url = new Uri("http://localhost/doc")
                            }
                        },
                        new OpenApiTag
                        {
                            Description = "test description",
                            Name = "Tag2",
                            ExternalDocs = new OpenApiExternalDocs
                            {
                                Description = "test description",
                                Url = new Uri("http://localhost/doc")
                            }
                        }
                    },
                    Components = new OpenApiComponents
                    {
                        Schemas = new Dictionary<string, OpenApiSchema>
                        {
                            ["schemaObject1"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property7"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject2"
                                        }
                                    }
                                }
                            },
                            ["schemaObject2"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property5"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject1"
                                        }
                                    }
                                }
                            }
                        },
                        SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
                        {
                            {
                                "scheme1", new OpenApiSecurityScheme
                                {
                                    Description = "Test",
                                    Name = "Test",
                                    Flows = new OpenApiOAuthFlows
                                    {
                                        Implicit = new OpenApiOAuthFlow
                                        {
                                            AuthorizationUrl = new Uri("http://localhost/1")
                                        },
                                        AuthorizationCode = new OpenApiOAuthFlow
                                        {
                                            AuthorizationUrl = new Uri("http://localhost/2")
                                        }
                                    }
                                }
                            },
                            {
                                "scheme2", new OpenApiSecurityScheme
                                {
                                    Description = "Test",
                                    Name = "Test"
                                }
                            },
                            {
                                "scheme3", new OpenApiSecurityScheme
                                {
                                    Description = "Test",
                                    Name = "Test"
                                }
                            }
                        }
                    },
                    SecurityRequirements = new List<OpenApiSecurityRequirement>
                    {
                        new OpenApiSecurityRequirement
                        {
                            [
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "scheme1"
                                    }
                                }
                            ] = new List<string>()
                        },
                        new OpenApiSecurityRequirement
                        {
                            [
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "scheme2"
                                    }
                                }
                            ] = new List<string>()
                        },
                        new OpenApiSecurityRequirement
                        {
                            [
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "scheme3"
                                    }
                                }
                            ] = new List<string>()
                        }
                    }
                },
                new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {
                            "/test", new OpenApiPathItem
                            {
                                Summary = "test",
                                Description = "test",
                                Operations = new Dictionary<OperationType, OpenApiOperation>
                                {
                                    {
                                        OperationType.Get, new OpenApiOperation
                                        {
                                            RequestBody = new OpenApiRequestBody
                                            {
                                                Description = "description",
                                                Required = true,
                                                Content =
                                                {
                                                    ["application/xml"] = new OpenApiMediaType
                                                    {
                                                        Schema = new OpenApiSchema
                                                        {
                                                            Reference = new OpenApiReference
                                                            {
                                                                Id = "schemaObject1",
                                                                Type = ReferenceType.Schema
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            Responses = new OpenApiResponses
                                            {
                                                {
                                                    "200",
                                                    new OpenApiResponse
                                                    {
                                                        Description = "An updated complex object array response",
                                                        Content =
                                                        {
                                                            ["application/json"] = new OpenApiMediaType
                                                            {
                                                                Schema = new OpenApiSchema
                                                                {
                                                                    Type = "array",
                                                                    Items = new OpenApiSchema
                                                                    {
                                                                        Reference = new OpenApiReference
                                                                        {
                                                                            Type = ReferenceType.Schema,
                                                                            Id = "schemaObject1"
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            Security = new List<OpenApiSecurityRequirement>
                                            {
                                                new OpenApiSecurityRequirement
                                                {
                                                    {
                                                        new OpenApiSecurityScheme
                                                        {
                                                            Reference = new OpenApiReference
                                                            {
                                                                Type = ReferenceType.SecurityScheme,
                                                                Id = "scheme1"
                                                            }
                                                        },
                                                        new List<string>()
                                                    }
                                                },
                                                new OpenApiSecurityRequirement
                                                {
                                                    [
                                                        new OpenApiSecurityScheme
                                                        {
                                                            Reference = new OpenApiReference
                                                            {
                                                                Type = ReferenceType.SecurityScheme,
                                                                Id = "scheme4"
                                                            }
                                                        }
                                                    ] = new List<string>()
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Tags = new List<OpenApiTag>
                    {
                        new OpenApiTag
                        {
                            Description = "test description updated",
                            Name = "Tag1",
                            ExternalDocs = new OpenApiExternalDocs
                            {
                                Description = "test description",
                                Url = new Uri("http://localhost/doc")
                            }
                        }
                    },
                    Components = new OpenApiComponents
                    {
                        Schemas = new Dictionary<string, OpenApiSchema>
                        {
                            ["schemaObject1"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property7"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject2"
                                        }
                                    }
                                }
                            },
                            ["schemaObject2"] = new OpenApiSchema
                            {
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["property2"] = new OpenApiSchema
                                    {
                                        Type = "integer"
                                    },
                                    ["property5"] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        MaxLength = 15
                                    },
                                    ["property6"] = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema,
                                            Id = "schemaObject1"
                                        }
                                    }
                                }
                            }
                        },
                        SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
                        {
                            {
                                "scheme1", new OpenApiSecurityScheme
                                {
                                    Description = "Test",
                                    Name = "Test",
                                    Flows = new OpenApiOAuthFlows
                                    {
                                        Implicit = new OpenApiOAuthFlow
                                        {
                                            AuthorizationUrl = new Uri("http://localhost/3")
                                        },
                                        ClientCredentials = new OpenApiOAuthFlow
                                        {
                                            AuthorizationUrl = new Uri("http://localhost/2")
                                        }
                                    }
                                }
                            },
                            {
                                "scheme2", new OpenApiSecurityScheme
                                {
                                    Description = "Test",
                                    Name = "Test"
                                }
                            },
                            {
                                "scheme4", new OpenApiSecurityScheme
                                {
                                    Description = "Test",
                                    Name = "Test"
                                }
                            }
                        }
                    },
                    SecurityRequirements = new List<OpenApiSecurityRequirement>
                    {
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "scheme1"
                                    }
                                },
                                new List<string>()
                            },
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "scheme2"
                                    }
                                },
                                new List<string>()
                            }
                        },
                        new OpenApiSecurityRequirement
                        {
                            [
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "scheme4"
                                    }
                                }
                            ] = new List<string>()
                        }
                    }
                },
                new List<OpenApiDifference>
                {
                    new OpenApiDifference
                    {
                        Pointer = "#/security/0/scheme2",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(IList<string>),
                        SourceValue = null,
                        TargetValue = new List<string>()
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/security/1/scheme4",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(IList<string>),
                        SourceValue = null,
                        TargetValue = new List<string>()
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/securitySchemes/scheme4",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSecurityScheme),
                        SourceValue = null,
                        TargetValue = new OpenApiSecurityScheme
                        {
                            Description = "Test",
                            Name = "Test"
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/security/1",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Add,
                        OpenApiComparedElementType = typeof(OpenApiSecurityRequirement),
                        SourceValue = null,
                        TargetValue = new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "scheme4"
                                    }
                                },
                                new List<string>()
                            }
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/tags/0/description",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(string),
                        SourceValue = "test description",
                        TargetValue = "test description updated"
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/components/securitySchemes/scheme1/flows/implicit/authorizationUrl",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(Uri),
                        SourceValue = "http://localhost/1",
                        TargetValue = "http://localhost/3"
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get/security/0/scheme1/flows/implicit/authorizationUrl",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(Uri),
                        SourceValue = "http://localhost/1",
                        TargetValue = "http://localhost/3"
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/security/0/scheme1/flows/implicit/authorizationUrl",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(Uri),
                        SourceValue = "http://localhost/1",
                        TargetValue = "http://localhost/3"
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/securitySchemes/scheme1/flows/clientCredentials",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(OpenApiOAuthFlow),
                        SourceValue = null,
                        TargetValue = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("http://localhost/2")
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/paths/~1test/get/security/0/scheme1/flows/clientCredentials",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(OpenApiOAuthFlow),
                        SourceValue = null,
                        TargetValue = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("http://localhost/2")
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/security/0/scheme1/flows/clientCredentials",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(OpenApiOAuthFlow),
                        SourceValue = null,
                        TargetValue = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("http://localhost/2")
                        }
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/components/securitySchemes/scheme1/flows/authorizationCode",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(OpenApiOAuthFlow),
                        SourceValue = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("http://localhost/2")
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/paths/~1test/get/security/0/scheme1/flows/authorizationCode",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(OpenApiOAuthFlow),
                        SourceValue = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("http://localhost/2")
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/security/0/scheme1/flows/authorizationCode",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Update,
                        OpenApiComparedElementType = typeof(OpenApiOAuthFlow),
                        SourceValue = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("http://localhost/2")
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/components/securitySchemes/scheme3",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSecurityScheme),
                        SourceValue = new OpenApiSecurityScheme
                        {
                            Description = "Test",
                            Name = "Test"
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer = "#/tags/1",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiTag),
                        SourceValue = new OpenApiTag
                        {
                            Description = "test description",
                            Name = "Tag2",
                            ExternalDocs = new OpenApiExternalDocs
                            {
                                Description = "test description",
                                Url = new Uri("http://localhost/doc")
                            }
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/security/2",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(OpenApiSecurityRequirement),
                        SourceValue = new OpenApiSecurityRequirement
                        {
                            [
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "scheme3"
                                    }
                                }
                            ] = new List<string>()
                        },
                        TargetValue = null
                    },
                    new OpenApiDifference
                    {
                        Pointer =
                            "#/security/1/scheme2",
                        OpenApiDifferenceOperation = OpenApiDifferenceOperation.Remove,
                        OpenApiComparedElementType = typeof(IList<string>),
                        SourceValue = new List<string>(),
                        TargetValue = null
                    }
                }
            };
        }
    }
}
