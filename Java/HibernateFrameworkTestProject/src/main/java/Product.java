import javax.persistence.*;

/**
 * Created by master on 2017/01/28.
 */


@Entity
@Table(name = "Products")
public class Product {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Id")
    private int _id;
    @Column(name = "Cost")
    private int _cost;
    @Column(name = "Name")
    private String _name;
    @Column(name = "ManufacturerName")
    private String _manufacturerName;

    public int get_id() {
        return _id;
    }

    public void set_id(int _id) {
        this._id = _id;
    }

    public int get_cost() {
        return _cost;
    }

    public void set_cost(int _cost) {
        this._cost = _cost;
    }

    public String get_name() {
        return _name;
    }

    public void set_name(String _name) {
        this._name = _name;
    }

    public String get_manufacturerName() {
        return _manufacturerName;
    }

    public void set_manufacturerName(String _manufacturerName) {
        this._manufacturerName = _manufacturerName;
    }

    @Override
    public String toString() {
        return "Id: " + _id + " Name: " +_name + " Manufacturer: " + _manufacturerName + " Cost: " +  _cost;
    }

    public Product(int cost, String name, String manufacturerName)
    {
        _cost = cost;
        _name = name;
        _manufacturerName = manufacturerName;
    }

    public Product()
    {

    }
}
