/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package projectGutenbergTest;

import java.util.ArrayList;

/**
 *
 * @author Manse
 */
public class ExpectedOutputList {
    String country;
    ArrayList<ExpectedOutput> output;
    public ExpectedOutputList(String Country, ArrayList<ExpectedOutput> Output){
        country = Country;
        output = Output;
    }
}
