#include "Fraction.h"

Fraction::Fraction(int n, int d) : _n(n), _d(d) {
	if (d == 0) {
		throw Fraction::DivideByZeroError();
	}
}

int Fraction::safeAdd(int a, int b) {   
	if (a > INT_MAX - b) {
		throw Fraction::OverflowError();
	}
	return a + b;
}

int Fraction::safeMul(int a, int b) {
	if (a == 0 || b == 0) {
		return 0;
	}
	if (b > 0 && (a > INT_MAX / b || a < INT_MIN / b)) {
		throw Fraction::OverflowError();
	}
	if (b < 0 && (a < INT_MAX / b || a > INT_MIN / b)) {
		throw Fraction::OverflowError();
	}
	return a * b;
}

Fraction Fraction::operator+(const Fraction & f) {	
	int n = Fraction::safeAdd(
		Fraction::safeMul(_n, f._d),
		Fraction::safeMul(_d, f._n)
	);
	return Fraction(n, Fraction::safeMul(_d, f._d));
}

Fraction Fraction::operator*(const Fraction & f) {
	return Fraction(Fraction::safeMul(_n, f._n), Fraction::safeMul(_d, f._d));
}

double Fraction::eval() {
	return (double) _n / (double) _d;
}

const char * Fraction::Error::what(void) { return "Generic error in Fraction"; }
const char * Fraction::DivideByZeroError::what(void) { return "Division by zero in Fraction"; }
const char * Fraction::OverflowError::what(void) { return "Integer overflow in Fraction"; }
