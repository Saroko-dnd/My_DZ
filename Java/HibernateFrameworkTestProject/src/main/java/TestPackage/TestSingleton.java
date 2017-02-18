package TestPackage;

import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

/**
 * Created by admin on 18.02.2017.
 */
public class TestSingleton {
    private static ReentrantLock lock = new ReentrantLock();
    private static volatile TestSingleton ourInstance = null;

    public static TestSingleton getInstance() {
        if (ourInstance == null)
        {
            lock.lock();
            if(ourInstance == null)
            {
                ourInstance = new TestSingleton();
            }
            lock.unlock();
        }
        return ourInstance;
    }

    private TestSingleton() {
    }
}
