namespace Guyl.GLogger
{
	using System;

	/// <summary>
	/// Specifies that a field or property should appears in Unity dropdown as a log channel.
	/// </summary>
	/// <remarks>
	/// This attribute is not a hard requirement, but without it, your channel won't appear in GLogger custom editors.
	/// </remarks>
	[AttributeUsage( AttributeTargets.Field | AttributeTargets.Property )]
	public class LogChannelAttribute : Attribute { }
}