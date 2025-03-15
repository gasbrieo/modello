using Microsoft.AspNetCore.Mvc.Testing;
using Modello.Presentation;

namespace Modello.FunctionalTests.TestHelpers;

public class CustomWebApplicationFactory : WebApplicationFactory<IPresentationMarker>;
