package WebDBInterface.DBEntities;

//import org.codehaus.jackson.annotate.JsonManagedReference;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import javax.persistence.*;

/**
 * Created by admin on 01.04.2017.
 */
@Entity
@Table(name = "Products")
public class Product {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Product_Id")
    private int _id;
    @Column(name = "Price")
    private int _price;
    @Column(name = "Name")
    private String  _name;
    @Column(name = "Quantity")
    private int _quantity;
    @Column(name = "Color")
    private String _color;
    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "Producer_Id", nullable = false)
    @JsonManagedReference("ggg")
    private Producer _producer;

    public int getId() {
        return _id;
    }

    public void setId(int id) {
        _id = id;
    }

    public int getPrice() {
        return _price;
    }

    public void setPrice(int price) {
        _price = price;
    }

    public String getName() {
        return _name;
    }

    public void setName(String name) {
        _name = name;
    }

    public int getQuantity() {
        return _quantity;
    }

    public void setQuantity(int quantity) {
        _quantity = quantity;
    }

    public String getColor() {
        return _color;
    }

    public void setColor(String color) {
        _color = color;
    }

    public WebDBInterface.DBEntities.Producer getProducer() {
        return _producer;
    }

    public void setProducer(WebDBInterface.DBEntities.Producer producer) {
        _producer = producer;
    }

    public Product(){

    }

    public Product(int newPrice, String newName, int newQuantity, String newColor){
        _price = newPrice;
        _name = newName;
        _quantity = newQuantity;
        _color = newColor;
    }

}
