#ifndef FRACTION_H
#define FRACTION_H

#include <exception>

class Fraction {

private:
	int _n;
	int _d;

	static int safeAdd(int a, int b);
	static int safeMul(int a, int b);

public:
	Fraction(int n, int d = 1);
	
	Fraction operator+(const Fraction& f);
	Fraction operator*(const Fraction& f);

	double eval();

	// Exceptions

	class Error : public std::exception {
	public:
		virtual const char* what(void);
	};

	class DivideByZeroError : public Fraction::Error {
	public:
		virtual const char* what(void);
	};

	class OverflowError : public Fraction::Error {
	public:
		virtual const char* what(void);
	};

};

#endif