package WebDBInterface.DBEntities;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Set;

/**
 * Created by admin on 01.04.2017.
 */
@Entity
@Table(name = "Producers")
public class Producer {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Producer_Id")
    private int _id;
    @Column(name = "Name")
    private String _name;
    @Column(name = "Country")
    private String _country;
    @Column(name = "AnnualProfit")
    private int _annualProfit;
    @OneToMany(fetch = FetchType.LAZY, mappedBy = "Producer")
    private Set<Product> SetOfProducts = new HashSet<Product>(0);

    public int getId() {
        return _id;
    }

    public void setId(int id) {
        _id = id;
    }

    public String getName() {
        return _name;
    }

    public void setName(String name) {
        _name = name;
    }

    public String getCountry() {
        return _country;
    }

    public void setCountry(String country) {
        _country = country;
    }

    public int getAnnualProfit() {
        return _annualProfit;
    }

    public void setAnnualProfit(int annualProfit) {
        _annualProfit = annualProfit;
    }

    public Set<Product> getSetOfProducts() {
        return SetOfProducts;
    }

    public void setSetOfProducts(Set<Product> setOfProducts) {
        SetOfProducts = setOfProducts;
    }

    public Producer(){

    }

    public Producer(String newName, String newCountry, int newAnnualProfit){
        _name = newName;
        _country = newCountry;
        _annualProfit = newAnnualProfit;
    }
}
