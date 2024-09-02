namespace Playwright.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CriticalAttribute : CategoryAttribute {}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class E2EAttribute : CategoryAttribute {}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SingleFunctionalityAttribute : CategoryAttribute {}