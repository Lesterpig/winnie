#include "list.h"
#include <iostream>

template <class T> class Ensemble;

template <class T> std::istream& operator>>(std::istream& in, Ensemble<T>& eref);
template <class T> std::ostream& operator<<(std::ostream& out,const Ensemble<T>& eref);

template <class T> class Ensemble {
  private:
    List<T> list;
  public:
    Ensemble() {
      list = List<T>();
    }
    Ensemble(const Ensemble<T>& eref) {
      list = List<T>(eref.list);
    }
    void add(T elem) {
      if (!(list == elem)) {
        list.addElement(elem);
      }
    }
    void del(T elem) {
      if (list == elem) {
        list.delElement(elem);
      }
    }

		Ensemble<T> operator+(const Ensemble<T>& v) const {
      Ensemble<T> eref(*this);
      for (ListIterator<T> iterlst = v.list.beg(); !(iterlst.finished()); ++iterlst) {
        eref.add(iterlst.get());
      }
      return eref;
    }

		Ensemble<T> operator-(const Ensemble<T>& v) const {
      Ensemble<T> eref(*this);
      for (ListIterator<T> iterlst = v.list.beg(); !(iterlst.finished()); ++iterlst) {
        eref.del(iterlst.get());
      }
      return eref;
    }

		Ensemble<T> operator*(const Ensemble<T>& v) const {
      Ensemble<T> eref;
      for (ListIterator<T> iterlst = v.list.beg(); !(iterlst.finished()); ++iterlst) {
        if (list == iterlst.get()) {
          eref.add(iterlst.get());
        }
      }

      return eref;
    }

		Ensemble<T> operator/(const Ensemble<T>& v) const {
      return (*this + v) - (*this * v); 
    }
		
    friend std::istream& operator>> <T>(std::istream& in,Ensemble<T>& eref);
		friend std::ostream& operator<< <T>(std::ostream& out,const Ensemble<T>& eref);
};

template <class T> std::istream& operator>>(std::istream& in, Ensemble<T>& eref) {
  int nb;
  in >> nb;
  for (int i = 0; i < nb; i++) {
    T tmp;
    in >> tmp;
    eref.add(tmp);
  }
  return in;
}

template <class T> std::ostream& operator<<(std::ostream& out, const Ensemble<T>& eref) {
  out << eref.list;
  return out;
}
