package WebPackage.Aspects;

import org.apache.log4j.Logger;
import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.ProceedingJoinPoint;
import org.aspectj.lang.annotation.Around;
import org.aspectj.lang.annotation.Aspect;
import org.aspectj.lang.annotation.Before;
import org.springframework.stereotype.Component;

/**
 * Created by admin on 04.03.2017.
 */
@Aspect
@Component
public class MyLogger {

    final static Logger logger = Logger.getLogger(MyLogger.class);

    /*@Before("execution(* WebPackage.WebControllers.HelloController.ReturnStudent(..))")
    public void logBefore(JoinPoint joinPoint) {
        if(logger.isInfoEnabled())
        {
            logger.info("This is info : " + joinPoint.getArgs()[0].toString());
        }
        //System.out.println(joinPoint.getArgs()[0]);
    }*/

    @Around("execution(* WebPackage.WebControllers.HelloController.ReturnStudent(..))")
    public void logAround(ProceedingJoinPoint proceedingJoinPoint){
        logger.info("Argument of getObjectAsJson() : " + proceedingJoinPoint.getArgs()[0].toString());
        Object value = null;
        long startTime = System.currentTimeMillis();
        try {
            value = proceedingJoinPoint.proceed();
        } catch (Throwable e) {
            e.getMessage();
        }
        long stopTime = System.currentTimeMillis();
        long elapsedTime = stopTime - startTime;
        logger.info("After invoking getObjectAsJson() return value : "+ value + ". Execution time: " + elapsedTime);
    }
}
