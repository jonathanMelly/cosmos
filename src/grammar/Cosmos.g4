//langage de type 'pseudo-code' pour apprendre à programmero
parser grammar Cosmos ;


options { tokenVocab = CosmosLexer; }

//Removes clscompliant warning on build
@header {#pragma warning disable 3021}

programme : entete mainStart (instruction|noop)+ mainEnd .*? EOF ;

entete : auteur date entreprise description ;

auteur : LABEL_AUTEUR TEXTE_LIBRE_MONOLIGNE ;

date : LABEL_DATE CONTENU_DATE RETOUR_DE_CHARIOT ;

entreprise : ENTREPRISE_ENTETE TEXTE_LIBRE_MONOLIGNE ;

//Active le mode island dans le lexer
description : DESCRIPTION_ENTETE TEXTE_LIBRE_MULTILIGNE;

mainStart: DEBUT nomDuProgramme=MOT (BIBLIOTHEQUE bibliotheque=MOT)? DEUX_POINT ;
mainEnd: FIN DE_LA_TRANSMISSION? POINT;

instruction : TABULATION+ (instruction_simple | instruction_complexe) ;
noop:TABULATION* RETOUR_DE_CHARIOT ;

instruction_simple   : (afficher|allouer|affecter|recuperer|generer_aleatoire|placer_curseur|dormir|colorier|decouper) POINT RETOUR_DE_CHARIOT ; //terminaison identique pour chaque
instruction_complexe : selection|boucle ; //terminaison spécifique pour chaque

afficher : AFFICHER expression;
allouer : ALLOUER_TERME une_zone_memoire (INITIALISATION_TERME? expression)? ;
affecter : ( INSERER expression DANS la_zone_memoire) | (variable OPERATEUR_MATH_EGAL expression ) ;
recuperer: RECUPERER  la_zone_memoire;
placer_curseur: PLACER_LE_CURSEUR (ligne=LIGNE|colonne=COLONNE) expression_numerique;
generer_aleatoire:  PLACER_ALEATOIRE min=expression_numerique ET max=expression_numerique DANS la_zone_memoire;
dormir: ATTENDRE expression_numerique MS;
colorier: CHOISIR_COULEUR (red=ROUGE|green=VERT|blue=BLEU|white=BLANC|black=NOIR|gray=GRIS) dark=FONCE? POUR_LE (text=TEXTE|background=FOND);
decouper: DECOUPER source=expression SUR separateur=expression;


variable : (LA VALEUR DE)? la_zone_memoire;

la_zone_memoire : (LA ZONE_MEMOIRE ZONE_NOM?)? VARIABLE;
une_zone_memoire : (UNE ZONE_MEMOIRE ZONE_NOM?)? VARIABLE;

boucle :
     REPETER (expression_numerique FOIS | TANT_QUE expression_booleenne | boucle_avec_variable) RETOUR_DE_CHARIOT
    (instruction|noop)+
    TABULATION+ SUIVANT RETOUR_DE_CHARIOT ;

boucle_avec_variable : AUTANT_DE_FOIS VARIABLE | LE_NOMBRE_DE_FOIS  variable;


selection :
    SI base_si
    sinon_si*
    sinon?
    TABULATION+ POINT_INTERROGATION RETOUR_DE_CHARIOT ;

base_si : condition=expression_booleenne ALORS RETOUR_DE_CHARIOT (instruction|noop)+ ;
sinon_si : TABULATION+ SINON_SI base_si ;
sinon : TABULATION+ ET_SINON RETOUR_DE_CHARIOT (instruction|noop)+ ;

//Variable doublé pour éviter que la première règle de sous-expression prenne le dessus
expression
        : variable
        | expression_comparable
        | expression_booleenne
        ;

//Variable doublé pour éviter que la première règle de sous-expression prenne le dessus
expression_comparable
        : variable
        | expression_textuelle
        | expression_numerique
        ;

//Exprimée dans l'ordre de priorité des opérateurs
expression_booleenne
        : gauche=expression_booleenne operateur=OPERATEUR_LOGIQUE_OU droite=expression_booleenne
        | gauche=expression_booleenne operateur=(ET | OPERATEUR_LOGIQUE_ET | OPERATEUR_LOGIQUE_OU_EXCLUSIF) droite=expression_booleenne //priorité différente ?
        | gaucheNb=expression_comparable
            operateurNb=(  OPERATEUR_COMPARAISON_EQUIVALENT
                         | OPERATEUR_COMPARAISON_DIFFERENT
                         | OPERATEUR_COMPARAISON_PLUS_GRAND
                         | OPERATEUR_COMPARAISON_PLUS_GRAND_OU_EGAL
                         | OPERATEUR_COMPARAISON_PLUS_PETIT
                         | OPERATEUR_COMPARAISON_PLUS_PETIT_OU_EGAL)
          droiteNb=expression_comparable
        | gauche=expression_booleenne operateur=(OPERATEUR_COMPARAISON_EQUIVALENT | OPERATEUR_COMPARAISON_DIFFERENT) droite=expression_booleenne
        | gauche=expression_booleenne operateur=OPERATEUR_LOGIQUE_EST (VRAI|FAUX) //construction sympa ;-)
        | OPERATEUR_LOGIQUE_NON sousExpression=expression_booleenne
        | (VRAI | FAUX | variable)
        | PARENTHESE_GAUCHE sousExpression=expression_booleenne PARENTHESE_DROITE
        ;

//Exprimée dans l'ordre de priorité des opérateurs
expression_numerique
        : gauche=expression_numerique operateur=(OPERATEUR_MATH_PUISSANCE | OPERATEUR_MATH_RACINE_CARREE) droite=expression_numerique
        | gauche=expression_numerique operateur=(OPERATEUR_MATH_FOIS | OPERATEUR_MATH_DIVISE) droite=expression_numerique
        | gauche=expression_numerique operateur=(OPERATEUR_MATH_PLUS | OPERATEUR_MATH_MOINS) droite=expression_numerique
        | (atome_numerique | variable)
        | operateur=(OPERATEUR_MATH_PLUS | OPERATEUR_MATH_MOINS) sousExpression=expression_numerique
        | PARENTHESE_GAUCHE sousExpression=expression_numerique PARENTHESE_DROITE
        ;

expression_textuelle : atome_textuel ;

atome_textuel : chaine_de_caractere ;
atome_numerique : nombre ;

chaine_de_caractere : LE_TEXTE? VALEUR_TEXTE ;

nombre : (LE_NOMBRE|LA VALEUR)? VALEUR_NOMBRE ;