grammar LabCalculator;

/*
 * Parser Rules
 */

compileUnit : expression EOF;

expression :
	LPAREN expression RPAREN #ParenthesizedExpr
	| expression EXPONENT expression #ExponentialExpr
    | operatorToken =(ADD | SUBTRACT) expression #UnaryExpr
    | expression operatorToken=(MULTIPLY | DIVIDE) expression #MultiplicativeExpr
    | expression operatorToken=(ADD | SUBTRACT) expression #AdditiveExpr
    | expression comparisonOperator expression #ComparisonExpr
	| NUMBER #NumberExpr
	| IDENTIFIER #IdentifierExpr
	; 
    comparisonOperator : 
    EQ | NEQ | LT | GT | LTE | GTE
    ;

/*
 * Lexer Rules (Tokens)
 */
 
 NUMBER : INT ('.' INT)?; 
IDENTIFIER : [a-zA-Z]+[1-9][0-9]*;

INT : ('0'..'9')+;

EXPONENT : '^';
MULTIPLY : '*';
DIVIDE : '/';
SUBTRACT : '-';
ADD : '+';

EQ : '=';
NEQ : '<>';
LTE : '<=';
GTE : '>=';
LT : '<';
GT : '>';

LPAREN : '(';
RPAREN : ')';

WS : [ \t\r\n] -> channel(HIDDEN);
