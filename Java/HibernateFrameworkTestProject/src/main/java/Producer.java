import org.hibernate.annotations.Cache;
import org.hibernate.annotations.CacheConcurrencyStrategy;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Set;

/**
 * Created by admin on 04.02.2017.
 */
@Entity
@Table(name = "Producers")
@Cache(usage = CacheConcurrencyStrategy.READ_WRITE)
public class Producer {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Producer_Id")
    private int _id;
    @Column(name = "Name")
    private String _name;
    @Column(name = "SiteLink")
    private String _siteLink;
    @Column(name = "FoundationYear")
    private int _foundationYear;
    @OneToMany(fetch = FetchType.LAZY, mappedBy = "_producer")
    private Set<Product> _setOfProducts = new HashSet<Product>(0);

    public Set<Product> get_setOfProducts() {
        return _setOfProducts;
    }

    public void set_setOfProducts(Set<Product> _setOfProducts) {
        this._setOfProducts = _setOfProducts;
    }

    public int get_id() {
        return _id;
    }

    public void set_id(int _id) {
        this._id = _id;
    }

    public String get_name() {
        return _name;
    }

    public void set_name(String _name) {
        this._name = _name;
    }

    public String get_siteLink() {
        return _siteLink;
    }

    public void set_siteLink(String _siteLink) {
        this._siteLink = _siteLink;
    }

    public int get_foundationYearr() {
        return _foundationYear;
    }

    public void set_foundationYearr(int _foundationYearr) {
        this._foundationYear = _foundationYearr;
    }

    public Producer(int foundationYear, String name, String siteLink)
    {
        _foundationYear = foundationYear;
        _name = name;
        _siteLink = siteLink;
    }

    public Producer()
    {

    }
}
