package WebPackage;

import java.io.IOException;

/**
 * Created by admin on 11.02.2017.
 */
//@javax.servlet.annotation.WebServlet(name = "Servlet")
public class Servlet extends javax.servlet.http.HttpServlet {
    protected void doPost(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse response) throws javax.servlet.ServletException, IOException {

    }

    protected void doGet(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse response) throws javax.servlet.ServletException, IOException {

        response.setContentType("text/html; charset=UTF-8");
        response.setCharacterEncoding("UTF-8");
        response.getWriter().println("Введите два числа и сервер вернет сумму!"/*FirstNumber + SecondNumber*/);

        response.getWriter().println("<form action=\"TestUrlForTestServlet\">");

        response.getWriter().println("<input name=\"FirstNumber\" type=\"number\">");
        response.getWriter().println("<input name=\"SecondNumber\" type=\"number\">");
        response.getWriter().println("<input type=\"submit\">");

        response.getWriter().println("</form>");
        if (request.getParameter("FirstNumber") != null && request.getParameter("SecondNumber") != null)
        {
            int FirstNumber = Integer.valueOf(request.getParameter("FirstNumber").toString());
            int SecondNumber = Integer.valueOf(request.getParameter("SecondNumber").toString());
            int Result = FirstNumber + SecondNumber;
            response.getWriter().println(FirstNumber + " + " + SecondNumber + " = " + Result);
        }

    }
}
