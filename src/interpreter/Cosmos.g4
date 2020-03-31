//langage de type 'pseudo-code' pour apprendre à programmer
grammar Cosmos ;

programme : entete NEWLINE+ DEBUT NEWLINE+ INSTRUCTION*  FIN EOF ;

entete : AUTEUR NEWLINE DATE NEWLINE LIEU NEWLINE DESCRIPTION ;

AUTEUR : 'Auteur:' SPACE? STRING+  ;
DATE : 'Date:' SPACE? STRING+ ;
LIEU : 'Lieu:' SPACE? STRING+ ;
DESCRIPTION : 'Description:' SPACE? STRING{5,} ;

//TODO : gérer les namespace d'une façon sympa

//TODO : nom du programme (en plus de description ?)

DEBUT : 'Il était une fois...' ;

FIN   : 'THE END' ;

INSTRUCTION : ('HelloWorld' | 'Afficher le message "Bonjour Monde"') SPACE* POINT NEWLINE* ;

NEWLINE : '\r'? '\n' ;
STRING : CHAR+ ;
fragment CHAR : ~["\\\r\n ] ;

POINT: '.' ;

SPACE: ' ' ;

//WS: [ \t\r\n\u000C]+ -> channel(HIDDEN);
//WS : [ \r\n\t] + -> skip ;
WS : [ \r\n\t] + -> channel(HIDDEN) ;