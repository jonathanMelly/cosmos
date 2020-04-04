﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Volumes/nFo/data/etml/codespace/csharp/cosmos/src/interpreter/Cosmos.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace interpreter.antlr {
#pragma warning disable 3021

using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="ICosmosListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class CosmosBaseListener : ICosmosListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.programme"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterProgramme([NotNull] CosmosParser.ProgrammeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.programme"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitProgramme([NotNull] CosmosParser.ProgrammeContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.entete"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEntete([NotNull] CosmosParser.EnteteContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.entete"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEntete([NotNull] CosmosParser.EnteteContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.auteur"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAuteur([NotNull] CosmosParser.AuteurContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.auteur"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAuteur([NotNull] CosmosParser.AuteurContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.date"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDate([NotNull] CosmosParser.DateContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.date"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDate([NotNull] CosmosParser.DateContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.entreprise"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEntreprise([NotNull] CosmosParser.EntrepriseContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.entreprise"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEntreprise([NotNull] CosmosParser.EntrepriseContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.description"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDescription([NotNull] CosmosParser.DescriptionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.description"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDescription([NotNull] CosmosParser.DescriptionContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.contexte"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterContexte([NotNull] CosmosParser.ContexteContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.contexte"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitContexte([NotNull] CosmosParser.ContexteContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.instruction"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterInstruction([NotNull] CosmosParser.InstructionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.instruction"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitInstruction([NotNull] CosmosParser.InstructionContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.afficher"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAfficher([NotNull] CosmosParser.AfficherContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.afficher"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAfficher([NotNull] CosmosParser.AfficherContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace interpreter.antlr