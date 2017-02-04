import org.hibernate.annotations.*;
import org.hibernate.annotations.Cache;

import javax.enterprise.inject.spi.*;
import javax.persistence.*;
import javax.persistence.Entity;
import javax.persistence.Table;
import java.beans.*;

/**
 * Created by master on 2017/01/28.
 */

@Entity
@Table(name = "Products")
@Cache(usage = CacheConcurrencyStrategy.READ_WRITE)
public class Product {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Product_Id")
    private int _id;
    @Column(name = "Cost")
    private int _cost;
    @Column(name = "Name")
    private String _name;
    private Producer _producer;

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

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "Producer_Id", nullable = false)
    public Producer get_producer() {
        return _producer;
    }

    public void set_producer(Producer _producer) {
        this._producer = _producer;
    }

    @Override
    public String toString() {
        return "Id: " + _id + " Name: " +_name + " Manufacturer: " + _producer.get_name() + " Cost: " +  _cost;
    }

    public Product(int cost, String name, Producer producer)
    {
        _cost = cost;
        _name = name;
        _producer = producer;
    }

    public Product()
    {

    }
}
