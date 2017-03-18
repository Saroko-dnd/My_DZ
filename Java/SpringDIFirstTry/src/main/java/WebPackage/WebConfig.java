package WebPackage;

import DBPackage.DB_DI_Providers.DBDIConfiguration;
import org.springframework.context.annotation.*;
import org.springframework.web.servlet.config.annotation.EnableWebMvc;
import org.springframework.web.servlet.config.annotation.ResourceHandlerRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurerAdapter;
import org.springframework.web.servlet.view.InternalResourceViewResolver;
import org.springframework.web.servlet.view.JstlView;

/**
 * Created by admin on 25.02.2017.
 */
@Configuration
@EnableWebMvc
@EnableAspectJAutoProxy
@ComponentScan({ "DBPackage", "WebPackage" })
public class WebConfig extends WebMvcConfigurerAdapter {

    public WebConfig() {
        System.out.println("WebConfig");
    }

    @Override
    public void addResourceHandlers(ResourceHandlerRegistry registry) {
        registry.addResourceHandler("/WEB-INF/Pages/**").addResourceLocations("/Pages/");
    }

    @Bean
    public InternalResourceViewResolver setupViewResolver() {
        InternalResourceViewResolver resolver = new InternalResourceViewResolver();
        resolver.setPrefix("/WEB-INF/Pages/");
        resolver.setSuffix(".jsp");
        return resolver;
    }
}
