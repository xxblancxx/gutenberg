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
public class ExpectedBookList {
    String input;
    ArrayList<ExpectedBook> output;
    public ExpectedBookList(String Input, ArrayList<ExpectedBook> Output){
        this.input = Input;
        output = Output;
    }
}
