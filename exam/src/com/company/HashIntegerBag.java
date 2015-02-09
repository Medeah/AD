package com.company;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by frederik on 2/9/15.
 */
public class HashIntegerBag implements IntegerBag {
    private Map<Integer, Integer> data;

    public HashIntegerBag() {
        data = new HashMap<Integer, Integer>();
    }

    public void add(int i) {
        int next = 1;
        if (data.containsKey(i)) {
            next = data.get(i) + 1;
        }
        data.put(i, next);
    }

    public int number_of(int i) {
        if (data.containsKey(i)) {
            return data.get(i);
        } else {
            return 0;
        }
    }

    public void delete(int i) {
        if (data.containsKey(i)) {
            data.put(i, data.get(i) - 1);
        }

        // do nothing if key not found
    }

    public boolean same_elements(IntegerBag C) {
        for (int a : data.keySet()) {
            if (C.number_of(a) == 0) {
                return false;
            }
        }
        return true;
    }
}
