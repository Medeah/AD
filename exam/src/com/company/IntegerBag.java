package com.company;

/**
 * Created by frederik on 2/9/15.
 */
public interface IntegerBag {

    //add  i to the multiset
    void add(int i);

    //returns the number of occurrences of i in the multiset
    int number_of(int i);

    //deletes one occurrence of i from the multiset (does nothing if the multiset does not contain any i)
    void delete(int i);

    //returns true if this multiset and the multiset C contain the same elements (but maybe with different number of occurrences)
    boolean same_elements(IntegerBag C);
}
