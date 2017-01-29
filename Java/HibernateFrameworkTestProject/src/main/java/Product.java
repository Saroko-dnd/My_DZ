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
    public int Id;
    @Column(name = "Cost")
    public int Cost;
    @Column(name = "Name")
    public String Name;
    @Column(name = "ManufacturerName")
    public String ManufacturerName;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public int getCost() {
        return Cost;
    }

    public void setCost(int cost) {
        Cost = cost;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getManufacturerName() {
        return ManufacturerName;
    }

    public void setManufacturerName(String manufacturerName) {
        ManufacturerName = manufacturerName;
    }

    public Product(int cost, String name, String manufacturerName)
    {
        Cost = cost;
        Name = name;
        ManufacturerName = manufacturerName;
    }

    public Product()
    {

    }
}
