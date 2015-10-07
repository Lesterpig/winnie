#include <assert.h>
#include <iostream>
#include "Fraction.h"
#include "sequences.h"

void testFraction() {
	// Test constructors

	Fraction a = Fraction(2);
	Fraction b = Fraction(5, 2);
	Fraction c = Fraction(-1, 5);

	assert(a.eval() == 2);
	assert(b.eval() == 2.5);
	assert(c.eval() == -0.2);

	// Test operators

	Fraction sum = a + b;
	Fraction mul = a * b;

	assert(sum.eval() == 4.5);
	assert(mul.eval() == 5);

	// Test exceptions

	bool passed;
	passed = false;
	try {
		Fraction zero = Fraction(1, 0);
	}
	catch (Fraction::DivideByZeroError) {
		passed = true;
	}
	assert(passed);

	passed = false;
	try {
		Fraction huge = Fraction(INT_MAX - 1) + a;
	}
	catch (Fraction::OverflowError) {
		passed = true;
	}
	assert(passed);
}

void testSeq() {

	try {
		std::string s1, s2, s3;

		std::cout << "Entrez une sequence proteique: ";
		std::cin >> s1;
		std::cout << "Entrez son nom: ";
		std::cin >> s2;

		seqprot P1(s1, s2);
		std::cout << P1 << std::endl;

		std::cout << "Entrez une sequence d'adn: ";
		std::cin >> s1;
		std::cout << "Entrez son nom: ";
		std::cin >> s2;

		seqadn A1(s1, s2);
		std::cout << A1 << std::endl;

		std::cout << "Entrez une sequence d'arn: ";
		std::cin >> s1;
		std::cout << "Entrez son nom: ";
		std::cin >> s2;

		seqarn R1(s1, s2);
		std::cout << R1 << std::endl;
	}
	catch (seqmac::UnknownCharError e) {
		std::cout << "ERROR: illegal character '" << e.getChar() << "' in sequence." << std::endl;
	}
}

int main(int argv, char** argc) {

	std::cout << "Testing Fraction..." << std::endl;
	testFraction();
	std::cout << "Testing Sequences..." << std::endl;
	testSeq();
	std::cout << "All tests passed!" << std::endl;

	system("pause");

	return 0;
}