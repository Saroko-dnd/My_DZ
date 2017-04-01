package WebDBInterface.WebPart;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.EnableAspectJAutoProxy;
import org.springframework.web.servlet.config.annotation.EnableWebMvc;
import org.springframework.web.servlet.config.annotation.ResourceHandlerRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurerAdapter;
import org.springframework.web.servlet.view.InternalResourceViewResolver;

/**
 * Created by admin on 01.04.2017.
 */
@Configuration
@EnableWebMvc
//@EnableAspectJAutoProxy
@ComponentScan({ "WebDBInterface.DB_DI_Providers", "WebDBInterface.WebPart.Controllers"})
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
