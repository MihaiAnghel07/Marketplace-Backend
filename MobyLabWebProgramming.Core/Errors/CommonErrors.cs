﻿using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
    public static ErrorMessage CarNotFound => new(HttpStatusCode.NotFound, "Car doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ContactNotFound => new(HttpStatusCode.NotFound, "Contact doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FeatureNotFound => new(HttpStatusCode.NotFound, "Feature doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage MessageNotFound => new(HttpStatusCode.NotFound, "Message doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage CarFeaturesNotFound => new(HttpStatusCode.NotFound, "CarFeatures doesn't exist!", ErrorCodes.EntityNotFound);
}
