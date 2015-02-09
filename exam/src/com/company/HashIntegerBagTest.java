package com.company;

import junit.framework.Assert;
import junit.framework.TestCase;

public class HashIntegerBagTest extends TestCase {

    public void testNumber_of() throws Exception {
        IntegerBag B = new HashIntegerBag();
        B.add(0);
        B.add(1);
        B.add(2);
        B.add(1);
        B.add(3);
        B.add(0);
        B.add(1);
        Assert.assertEquals(3, B.number_of(1));
        Assert.assertEquals(1, B.number_of(2));
    }

    public void testDelete() throws Exception {
        IntegerBag B = new HashIntegerBag();
        B.add(0);
        B.add(0);
        Assert.assertEquals(2, B.number_of(0));
        B.delete(0);
        Assert.assertEquals(1, B.number_of(0));
    }

    public void testSame_elements() throws Exception {
        IntegerBag B = new HashIntegerBag();
        B.add(0);
        B.add(1);
        B.add(2);
        B.add(1);
        B.add(3);
        B.add(0);
        B.add(1);

        IntegerBag C = new HashIntegerBag();
        C.add(0);
        C.add(1);
        C.add(2);
        C.add(2);
        C.add(2);
        C.add(3);
        C.add(3);
        Assert.assertTrue(B.same_elements(C));

        IntegerBag D = new HashIntegerBag();
        D.add(0);
        D.add(2);
        D.add(2);
        D.add(3);
        Assert.assertFalse(B.same_elements(D));
    }
}