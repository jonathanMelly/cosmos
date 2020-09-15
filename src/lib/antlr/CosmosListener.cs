﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Volumes/nFo/data/etml/codespace/csharp/cosmos/src/lib/../grammar/Cosmos.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace lib.antlr {
#pragma warning disable 3021
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="Cosmos"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface ICosmosListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.programme"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgramme([NotNull] Cosmos.ProgrammeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.programme"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgramme([NotNull] Cosmos.ProgrammeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.entete"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEntete([NotNull] Cosmos.EnteteContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.entete"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEntete([NotNull] Cosmos.EnteteContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.auteur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAuteur([NotNull] Cosmos.AuteurContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.auteur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAuteur([NotNull] Cosmos.AuteurContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.date"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDate([NotNull] Cosmos.DateContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.date"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDate([NotNull] Cosmos.DateContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.entreprise"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEntreprise([NotNull] Cosmos.EntrepriseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.entreprise"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEntreprise([NotNull] Cosmos.EntrepriseContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.description"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDescription([NotNull] Cosmos.DescriptionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.description"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDescription([NotNull] Cosmos.DescriptionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.mainStart"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMainStart([NotNull] Cosmos.MainStartContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.mainStart"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMainStart([NotNull] Cosmos.MainStartContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.mainEnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMainEnd([NotNull] Cosmos.MainEndContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.mainEnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMainEnd([NotNull] Cosmos.MainEndContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.instruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInstruction([NotNull] Cosmos.InstructionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.instruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInstruction([NotNull] Cosmos.InstructionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.noop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNoop([NotNull] Cosmos.NoopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.noop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNoop([NotNull] Cosmos.NoopContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.instruction_simple"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInstruction_simple([NotNull] Cosmos.Instruction_simpleContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.instruction_simple"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInstruction_simple([NotNull] Cosmos.Instruction_simpleContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.instruction_complexe"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInstruction_complexe([NotNull] Cosmos.Instruction_complexeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.instruction_complexe"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInstruction_complexe([NotNull] Cosmos.Instruction_complexeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.afficher"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAfficher([NotNull] Cosmos.AfficherContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.afficher"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAfficher([NotNull] Cosmos.AfficherContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.allouer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAllouer([NotNull] Cosmos.AllouerContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.allouer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAllouer([NotNull] Cosmos.AllouerContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.affecter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAffecter([NotNull] Cosmos.AffecterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.affecter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAffecter([NotNull] Cosmos.AffecterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.recuperer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRecuperer([NotNull] Cosmos.RecupererContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.recuperer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRecuperer([NotNull] Cosmos.RecupererContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.placer_curseur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPlacer_curseur([NotNull] Cosmos.Placer_curseurContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.placer_curseur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPlacer_curseur([NotNull] Cosmos.Placer_curseurContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.generer_aleatoire"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGenerer_aleatoire([NotNull] Cosmos.Generer_aleatoireContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.generer_aleatoire"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGenerer_aleatoire([NotNull] Cosmos.Generer_aleatoireContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.dormir"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDormir([NotNull] Cosmos.DormirContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.dormir"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDormir([NotNull] Cosmos.DormirContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.colorier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterColorier([NotNull] Cosmos.ColorierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.colorier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitColorier([NotNull] Cosmos.ColorierContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.decouper"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDecouper([NotNull] Cosmos.DecouperContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.decouper"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDecouper([NotNull] Cosmos.DecouperContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.afficher_curseur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAfficher_curseur([NotNull] Cosmos.Afficher_curseurContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.afficher_curseur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAfficher_curseur([NotNull] Cosmos.Afficher_curseurContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.masquer_curseur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMasquer_curseur([NotNull] Cosmos.Masquer_curseurContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.masquer_curseur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMasquer_curseur([NotNull] Cosmos.Masquer_curseurContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariable([NotNull] Cosmos.VariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariable([NotNull] Cosmos.VariableContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.la_zone_memoire"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLa_zone_memoire([NotNull] Cosmos.La_zone_memoireContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.la_zone_memoire"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLa_zone_memoire([NotNull] Cosmos.La_zone_memoireContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.une_zone_memoire"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUne_zone_memoire([NotNull] Cosmos.Une_zone_memoireContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.une_zone_memoire"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUne_zone_memoire([NotNull] Cosmos.Une_zone_memoireContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.boucle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBoucle([NotNull] Cosmos.BoucleContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.boucle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBoucle([NotNull] Cosmos.BoucleContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.boucle_avec_variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBoucle_avec_variable([NotNull] Cosmos.Boucle_avec_variableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.boucle_avec_variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBoucle_avec_variable([NotNull] Cosmos.Boucle_avec_variableContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.selection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSelection([NotNull] Cosmos.SelectionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.selection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSelection([NotNull] Cosmos.SelectionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.base_si"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBase_si([NotNull] Cosmos.Base_siContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.base_si"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBase_si([NotNull] Cosmos.Base_siContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.sinon_si"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSinon_si([NotNull] Cosmos.Sinon_siContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.sinon_si"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSinon_si([NotNull] Cosmos.Sinon_siContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.sinon"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSinon([NotNull] Cosmos.SinonContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.sinon"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSinon([NotNull] Cosmos.SinonContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] Cosmos.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] Cosmos.ExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.expression_comparable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_comparable([NotNull] Cosmos.Expression_comparableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.expression_comparable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_comparable([NotNull] Cosmos.Expression_comparableContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.expression_booleenne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_booleenne([NotNull] Cosmos.Expression_booleenneContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.expression_booleenne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_booleenne([NotNull] Cosmos.Expression_booleenneContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.expression_numerique"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_numerique([NotNull] Cosmos.Expression_numeriqueContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.expression_numerique"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_numerique([NotNull] Cosmos.Expression_numeriqueContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.expression_textuelle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_textuelle([NotNull] Cosmos.Expression_textuelleContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.expression_textuelle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_textuelle([NotNull] Cosmos.Expression_textuelleContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.atome_textuel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtome_textuel([NotNull] Cosmos.Atome_textuelContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.atome_textuel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtome_textuel([NotNull] Cosmos.Atome_textuelContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.atome_numerique"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtome_numerique([NotNull] Cosmos.Atome_numeriqueContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.atome_numerique"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtome_numerique([NotNull] Cosmos.Atome_numeriqueContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.chaine_de_caractere"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterChaine_de_caractere([NotNull] Cosmos.Chaine_de_caractereContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.chaine_de_caractere"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitChaine_de_caractere([NotNull] Cosmos.Chaine_de_caractereContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="Cosmos.nombre"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNombre([NotNull] Cosmos.NombreContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Cosmos.nombre"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNombre([NotNull] Cosmos.NombreContext context);
}
} // namespace lib.antlr
