using Quartz.Shared.Helpers;

namespace Quartz.Domain.Evaluating;

internal class ModuleBuilder(Module module, Scope location)
{
	public ModuleBuilder DeclareClass(string name, Action<ClassBuilder> configure)
	{
		Scope scope = name.Equals(RuntimeBuilder.NameWorkspace)
			? RuntimeBuilder.Workspace
			: location.GetSubscope(name);
		Class type = module.RegisterClass(name, scope, ~Position.Zero);
		configure(new ClassBuilder(type, scope));
		return this;
	}
}
