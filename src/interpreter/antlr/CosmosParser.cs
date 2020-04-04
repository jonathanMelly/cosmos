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
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class CosmosParser : Parser {
	public const int
		LABEL_AUTEUR=1, LABEL_DATE=2, CONTENU_DATE=3, ENTREPRISE_ENTETE=4, DESCRIPTION_ENTETE=5, 
		TITRE=6, CONTEXTE=7, DEBUT=8, FIN=9, COMMENTAIRE_LIGNE=10, COMMENTAIRE=11, 
		NOOP=12, FONCTION_AFFICHER=13, LE_TEXTE=14, LE_NOMBRE=15, VALEUR_TEXTE=16, 
		VALEUR_NOMBRE=17, MOT=18, VIRGULE=19, TAB=20, RETCHAR=21, POINT=22, ESPACE=23;
	public const int
		RULE_programme = 0, RULE_entete = 1, RULE_auteur = 2, RULE_date = 3, RULE_entreprise = 4, 
		RULE_description = 5, RULE_contexte = 6, RULE_instruction = 7, RULE_afficher = 8;
	public static readonly string[] ruleNames = {
		"programme", "entete", "auteur", "date", "entreprise", "description", 
		"contexte", "instruction", "afficher"
	};

	private static readonly string[] _LiteralNames = {
		null, "'Auteur:'", "'Date:'", null, "'Entreprise:'", "'Description:'", 
		null, null, "'Voici mes ordres:'", "'Fin.'", null, null, null, "'Afficher'", 
		null, null, null, null, null, "','", null, null, "'.'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "LABEL_AUTEUR", "LABEL_DATE", "CONTENU_DATE", "ENTREPRISE_ENTETE", 
		"DESCRIPTION_ENTETE", "TITRE", "CONTEXTE", "DEBUT", "FIN", "COMMENTAIRE_LIGNE", 
		"COMMENTAIRE", "NOOP", "FONCTION_AFFICHER", "LE_TEXTE", "LE_NOMBRE", "VALEUR_TEXTE", 
		"VALEUR_NOMBRE", "MOT", "VIRGULE", "TAB", "RETCHAR", "POINT", "ESPACE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Cosmos.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public CosmosParser(ITokenStream input)
		: base(input)
	{
		_interp = new ParserATNSimulator(this,_ATN);
	}
	public partial class ProgrammeContext : ParserRuleContext {
		public EnteteContext entete() {
			return GetRuleContext<EnteteContext>(0);
		}
		public ITerminalNode DEBUT() { return GetToken(CosmosParser.DEBUT, 0); }
		public ITerminalNode FIN() { return GetToken(CosmosParser.FIN, 0); }
		public ITerminalNode Eof() { return GetToken(CosmosParser.Eof, 0); }
		public ITerminalNode[] RETCHAR() { return GetTokens(CosmosParser.RETCHAR); }
		public ITerminalNode RETCHAR(int i) {
			return GetToken(CosmosParser.RETCHAR, i);
		}
		public ContexteContext contexte() {
			return GetRuleContext<ContexteContext>(0);
		}
		public InstructionContext[] instruction() {
			return GetRuleContexts<InstructionContext>();
		}
		public InstructionContext instruction(int i) {
			return GetRuleContext<InstructionContext>(i);
		}
		public ProgrammeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_programme; } }
		public override void EnterRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.EnterProgramme(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.ExitProgramme(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICosmosVisitor<TResult> typedVisitor = visitor as ICosmosVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProgramme(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ProgrammeContext programme() {
		ProgrammeContext _localctx = new ProgrammeContext(_ctx, State);
		EnterRule(_localctx, 0, RULE_programme);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 18; entete();
			State = 20;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				{
				State = 19; Match(RETCHAR);
				}
				}
				State = 22;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( _la==RETCHAR );
			State = 30;
			_errHandler.Sync(this);
			_la = _input.La(1);
			if (_la==CONTEXTE) {
				{
				State = 24; contexte();
				State = 26;
				_errHandler.Sync(this);
				_la = _input.La(1);
				do {
					{
					{
					State = 25; Match(RETCHAR);
					}
					}
					State = 28;
					_errHandler.Sync(this);
					_la = _input.La(1);
				} while ( _la==RETCHAR );
				}
			}

			State = 32; Match(DEBUT);
			State = 34;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				{
				State = 33; Match(RETCHAR);
				}
				}
				State = 36;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( _la==RETCHAR );
			State = 39;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				{
				State = 38; instruction();
				}
				}
				State = 41;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( _la==TAB );
			State = 43; Match(FIN);
			State = 44; Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class EnteteContext : ParserRuleContext {
		public AuteurContext auteur() {
			return GetRuleContext<AuteurContext>(0);
		}
		public ITerminalNode[] RETCHAR() { return GetTokens(CosmosParser.RETCHAR); }
		public ITerminalNode RETCHAR(int i) {
			return GetToken(CosmosParser.RETCHAR, i);
		}
		public DateContext date() {
			return GetRuleContext<DateContext>(0);
		}
		public EntrepriseContext entreprise() {
			return GetRuleContext<EntrepriseContext>(0);
		}
		public DescriptionContext description() {
			return GetRuleContext<DescriptionContext>(0);
		}
		public EnteteContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_entete; } }
		public override void EnterRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.EnterEntete(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.ExitEntete(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICosmosVisitor<TResult> typedVisitor = visitor as ICosmosVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEntete(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public EnteteContext entete() {
		EnteteContext _localctx = new EnteteContext(_ctx, State);
		EnterRule(_localctx, 2, RULE_entete);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 46; auteur();
			State = 47; Match(RETCHAR);
			State = 48; date();
			State = 49; Match(RETCHAR);
			State = 50; entreprise();
			State = 51; Match(RETCHAR);
			State = 52; description();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AuteurContext : ParserRuleContext {
		public ITerminalNode LABEL_AUTEUR() { return GetToken(CosmosParser.LABEL_AUTEUR, 0); }
		public ITerminalNode[] MOT() { return GetTokens(CosmosParser.MOT); }
		public ITerminalNode MOT(int i) {
			return GetToken(CosmosParser.MOT, i);
		}
		public AuteurContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_auteur; } }
		public override void EnterRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.EnterAuteur(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.ExitAuteur(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICosmosVisitor<TResult> typedVisitor = visitor as ICosmosVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAuteur(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AuteurContext auteur() {
		AuteurContext _localctx = new AuteurContext(_ctx, State);
		EnterRule(_localctx, 4, RULE_auteur);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 54; Match(LABEL_AUTEUR);
			State = 55; Match(MOT);
			State = 57;
			_errHandler.Sync(this);
			_la = _input.La(1);
			if (_la==MOT) {
				{
				State = 56; Match(MOT);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DateContext : ParserRuleContext {
		public ITerminalNode LABEL_DATE() { return GetToken(CosmosParser.LABEL_DATE, 0); }
		public ITerminalNode CONTENU_DATE() { return GetToken(CosmosParser.CONTENU_DATE, 0); }
		public DateContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_date; } }
		public override void EnterRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.EnterDate(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.ExitDate(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICosmosVisitor<TResult> typedVisitor = visitor as ICosmosVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDate(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DateContext date() {
		DateContext _localctx = new DateContext(_ctx, State);
		EnterRule(_localctx, 6, RULE_date);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 59; Match(LABEL_DATE);
			State = 60; Match(CONTENU_DATE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class EntrepriseContext : ParserRuleContext {
		public ITerminalNode ENTREPRISE_ENTETE() { return GetToken(CosmosParser.ENTREPRISE_ENTETE, 0); }
		public ITerminalNode MOT() { return GetToken(CosmosParser.MOT, 0); }
		public EntrepriseContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_entreprise; } }
		public override void EnterRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.EnterEntreprise(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.ExitEntreprise(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICosmosVisitor<TResult> typedVisitor = visitor as ICosmosVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEntreprise(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public EntrepriseContext entreprise() {
		EntrepriseContext _localctx = new EntrepriseContext(_ctx, State);
		EnterRule(_localctx, 8, RULE_entreprise);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 62; Match(ENTREPRISE_ENTETE);
			State = 63; Match(MOT);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DescriptionContext : ParserRuleContext {
		public ITerminalNode DESCRIPTION_ENTETE() { return GetToken(CosmosParser.DESCRIPTION_ENTETE, 0); }
		public ITerminalNode[] MOT() { return GetTokens(CosmosParser.MOT); }
		public ITerminalNode MOT(int i) {
			return GetToken(CosmosParser.MOT, i);
		}
		public ITerminalNode[] VIRGULE() { return GetTokens(CosmosParser.VIRGULE); }
		public ITerminalNode VIRGULE(int i) {
			return GetToken(CosmosParser.VIRGULE, i);
		}
		public ITerminalNode[] RETCHAR() { return GetTokens(CosmosParser.RETCHAR); }
		public ITerminalNode RETCHAR(int i) {
			return GetToken(CosmosParser.RETCHAR, i);
		}
		public DescriptionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_description; } }
		public override void EnterRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.EnterDescription(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.ExitDescription(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICosmosVisitor<TResult> typedVisitor = visitor as ICosmosVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDescription(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DescriptionContext description() {
		DescriptionContext _localctx = new DescriptionContext(_ctx, State);
		EnterRule(_localctx, 10, RULE_description);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 65; Match(DESCRIPTION_ENTETE);
			State = 67;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				{
				State = 66; Match(MOT);
				}
				}
				State = 69;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( _la==MOT );
			State = 80;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==VIRGULE) {
				{
				{
				State = 71; Match(VIRGULE);
				State = 72; Match(RETCHAR);
				State = 74;
				_errHandler.Sync(this);
				_la = _input.La(1);
				do {
					{
					{
					State = 73; Match(MOT);
					}
					}
					State = 76;
					_errHandler.Sync(this);
					_la = _input.La(1);
				} while ( _la==MOT );
				}
				}
				State = 82;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ContexteContext : ParserRuleContext {
		public ITerminalNode CONTEXTE() { return GetToken(CosmosParser.CONTEXTE, 0); }
		public ITerminalNode TITRE() { return GetToken(CosmosParser.TITRE, 0); }
		public ITerminalNode[] RETCHAR() { return GetTokens(CosmosParser.RETCHAR); }
		public ITerminalNode RETCHAR(int i) {
			return GetToken(CosmosParser.RETCHAR, i);
		}
		public ContexteContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_contexte; } }
		public override void EnterRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.EnterContexte(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.ExitContexte(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICosmosVisitor<TResult> typedVisitor = visitor as ICosmosVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitContexte(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ContexteContext contexte() {
		ContexteContext _localctx = new ContexteContext(_ctx, State);
		EnterRule(_localctx, 12, RULE_contexte);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 83; Match(CONTEXTE);
			State = 85;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				{
				State = 84; Match(RETCHAR);
				}
				}
				State = 87;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( _la==RETCHAR );
			State = 89; Match(TITRE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class InstructionContext : ParserRuleContext {
		public ITerminalNode POINT() { return GetToken(CosmosParser.POINT, 0); }
		public AfficherContext afficher() {
			return GetRuleContext<AfficherContext>(0);
		}
		public ITerminalNode[] TAB() { return GetTokens(CosmosParser.TAB); }
		public ITerminalNode TAB(int i) {
			return GetToken(CosmosParser.TAB, i);
		}
		public ITerminalNode[] RETCHAR() { return GetTokens(CosmosParser.RETCHAR); }
		public ITerminalNode RETCHAR(int i) {
			return GetToken(CosmosParser.RETCHAR, i);
		}
		public InstructionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_instruction; } }
		public override void EnterRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.EnterInstruction(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.ExitInstruction(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICosmosVisitor<TResult> typedVisitor = visitor as ICosmosVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInstruction(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public InstructionContext instruction() {
		InstructionContext _localctx = new InstructionContext(_ctx, State);
		EnterRule(_localctx, 14, RULE_instruction);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 92;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				{
				State = 91; Match(TAB);
				}
				}
				State = 94;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( _la==TAB );
			{
			State = 96; afficher();
			}
			State = 97; Match(POINT);
			State = 99;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				{
				State = 98; Match(RETCHAR);
				}
				}
				State = 101;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( _la==RETCHAR );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AfficherContext : ParserRuleContext {
		public ITerminalNode FONCTION_AFFICHER() { return GetToken(CosmosParser.FONCTION_AFFICHER, 0); }
		public ITerminalNode VALEUR_TEXTE() { return GetToken(CosmosParser.VALEUR_TEXTE, 0); }
		public ITerminalNode VALEUR_NOMBRE() { return GetToken(CosmosParser.VALEUR_NOMBRE, 0); }
		public ITerminalNode LE_TEXTE() { return GetToken(CosmosParser.LE_TEXTE, 0); }
		public ITerminalNode LE_NOMBRE() { return GetToken(CosmosParser.LE_NOMBRE, 0); }
		public AfficherContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_afficher; } }
		public override void EnterRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.EnterAfficher(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ICosmosListener typedListener = listener as ICosmosListener;
			if (typedListener != null) typedListener.ExitAfficher(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ICosmosVisitor<TResult> typedVisitor = visitor as ICosmosVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAfficher(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AfficherContext afficher() {
		AfficherContext _localctx = new AfficherContext(_ctx, State);
		EnterRule(_localctx, 16, RULE_afficher);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 103; Match(FONCTION_AFFICHER);
			State = 112;
			_errHandler.Sync(this);
			switch (_input.La(1)) {
			case LE_TEXTE:
			case VALEUR_TEXTE:
				{
				State = 105;
				_errHandler.Sync(this);
				_la = _input.La(1);
				if (_la==LE_TEXTE) {
					{
					State = 104; Match(LE_TEXTE);
					}
				}

				State = 107; Match(VALEUR_TEXTE);
				}
				break;
			case LE_NOMBRE:
			case VALEUR_NOMBRE:
				{
				State = 109;
				_errHandler.Sync(this);
				_la = _input.La(1);
				if (_la==LE_NOMBRE) {
					{
					State = 108; Match(LE_NOMBRE);
					}
				}

				State = 111; Match(VALEUR_NOMBRE);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x3\x19u\x4\x2\t\x2"+
		"\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4\t\t"+
		"\t\x4\n\t\n\x3\x2\x3\x2\x6\x2\x17\n\x2\r\x2\xE\x2\x18\x3\x2\x3\x2\x6\x2"+
		"\x1D\n\x2\r\x2\xE\x2\x1E\x5\x2!\n\x2\x3\x2\x3\x2\x6\x2%\n\x2\r\x2\xE\x2"+
		"&\x3\x2\x6\x2*\n\x2\r\x2\xE\x2+\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x5\x4<\n\x4\x3\x5\x3\x5"+
		"\x3\x5\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x6\a\x46\n\a\r\a\xE\aG\x3\a\x3\a\x3"+
		"\a\x6\aM\n\a\r\a\xE\aN\a\aQ\n\a\f\a\xE\aT\v\a\x3\b\x3\b\x6\bX\n\b\r\b"+
		"\xE\bY\x3\b\x3\b\x3\t\x6\t_\n\t\r\t\xE\t`\x3\t\x3\t\x3\t\x6\t\x66\n\t"+
		"\r\t\xE\tg\x3\n\x3\n\x5\nl\n\n\x3\n\x3\n\x5\np\n\n\x3\n\x5\ns\n\n\x3\n"+
		"\x2\x2\x2\v\x2\x2\x4\x2\x6\x2\b\x2\n\x2\f\x2\xE\x2\x10\x2\x12\x2\x2\x2"+
		"z\x2\x14\x3\x2\x2\x2\x4\x30\x3\x2\x2\x2\x6\x38\x3\x2\x2\x2\b=\x3\x2\x2"+
		"\x2\n@\x3\x2\x2\x2\f\x43\x3\x2\x2\x2\xEU\x3\x2\x2\x2\x10^\x3\x2\x2\x2"+
		"\x12i\x3\x2\x2\x2\x14\x16\x5\x4\x3\x2\x15\x17\a\x17\x2\x2\x16\x15\x3\x2"+
		"\x2\x2\x17\x18\x3\x2\x2\x2\x18\x16\x3\x2\x2\x2\x18\x19\x3\x2\x2\x2\x19"+
		" \x3\x2\x2\x2\x1A\x1C\x5\xE\b\x2\x1B\x1D\a\x17\x2\x2\x1C\x1B\x3\x2\x2"+
		"\x2\x1D\x1E\x3\x2\x2\x2\x1E\x1C\x3\x2\x2\x2\x1E\x1F\x3\x2\x2\x2\x1F!\x3"+
		"\x2\x2\x2 \x1A\x3\x2\x2\x2 !\x3\x2\x2\x2!\"\x3\x2\x2\x2\"$\a\n\x2\x2#"+
		"%\a\x17\x2\x2$#\x3\x2\x2\x2%&\x3\x2\x2\x2&$\x3\x2\x2\x2&\'\x3\x2\x2\x2"+
		"\')\x3\x2\x2\x2(*\x5\x10\t\x2)(\x3\x2\x2\x2*+\x3\x2\x2\x2+)\x3\x2\x2\x2"+
		"+,\x3\x2\x2\x2,-\x3\x2\x2\x2-.\a\v\x2\x2./\a\x2\x2\x3/\x3\x3\x2\x2\x2"+
		"\x30\x31\x5\x6\x4\x2\x31\x32\a\x17\x2\x2\x32\x33\x5\b\x5\x2\x33\x34\a"+
		"\x17\x2\x2\x34\x35\x5\n\x6\x2\x35\x36\a\x17\x2\x2\x36\x37\x5\f\a\x2\x37"+
		"\x5\x3\x2\x2\x2\x38\x39\a\x3\x2\x2\x39;\a\x14\x2\x2:<\a\x14\x2\x2;:\x3"+
		"\x2\x2\x2;<\x3\x2\x2\x2<\a\x3\x2\x2\x2=>\a\x4\x2\x2>?\a\x5\x2\x2?\t\x3"+
		"\x2\x2\x2@\x41\a\x6\x2\x2\x41\x42\a\x14\x2\x2\x42\v\x3\x2\x2\x2\x43\x45"+
		"\a\a\x2\x2\x44\x46\a\x14\x2\x2\x45\x44\x3\x2\x2\x2\x46G\x3\x2\x2\x2G\x45"+
		"\x3\x2\x2\x2GH\x3\x2\x2\x2HR\x3\x2\x2\x2IJ\a\x15\x2\x2JL\a\x17\x2\x2K"+
		"M\a\x14\x2\x2LK\x3\x2\x2\x2MN\x3\x2\x2\x2NL\x3\x2\x2\x2NO\x3\x2\x2\x2"+
		"OQ\x3\x2\x2\x2PI\x3\x2\x2\x2QT\x3\x2\x2\x2RP\x3\x2\x2\x2RS\x3\x2\x2\x2"+
		"S\r\x3\x2\x2\x2TR\x3\x2\x2\x2UW\a\t\x2\x2VX\a\x17\x2\x2WV\x3\x2\x2\x2"+
		"XY\x3\x2\x2\x2YW\x3\x2\x2\x2YZ\x3\x2\x2\x2Z[\x3\x2\x2\x2[\\\a\b\x2\x2"+
		"\\\xF\x3\x2\x2\x2]_\a\x16\x2\x2^]\x3\x2\x2\x2_`\x3\x2\x2\x2`^\x3\x2\x2"+
		"\x2`\x61\x3\x2\x2\x2\x61\x62\x3\x2\x2\x2\x62\x63\x5\x12\n\x2\x63\x65\a"+
		"\x18\x2\x2\x64\x66\a\x17\x2\x2\x65\x64\x3\x2\x2\x2\x66g\x3\x2\x2\x2g\x65"+
		"\x3\x2\x2\x2gh\x3\x2\x2\x2h\x11\x3\x2\x2\x2ir\a\xF\x2\x2jl\a\x10\x2\x2"+
		"kj\x3\x2\x2\x2kl\x3\x2\x2\x2lm\x3\x2\x2\x2ms\a\x12\x2\x2np\a\x11\x2\x2"+
		"on\x3\x2\x2\x2op\x3\x2\x2\x2pq\x3\x2\x2\x2qs\a\x13\x2\x2rk\x3\x2\x2\x2"+
		"ro\x3\x2\x2\x2s\x13\x3\x2\x2\x2\x11\x18\x1E &+;GNRY`gkor";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace interpreter.antlr