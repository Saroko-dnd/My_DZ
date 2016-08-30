<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <xsl:variable  name="Avg" select="(sum(//price) div count-not-empty(//price))"></xsl:variable>
    <h2>My CD Collection (price &lt; average price)
    <xsl:value-of select="$Avg" />
    
    </h2>
    <table border="1">
      <tr bgcolor="#9acd32">
        <th style="text-align:left">Title</th>
        <th style="text-align:left">Artist</th>
        <th style="text-align:left">Price</th>
      </tr>
      <xsl:for-each select="catalog/cd[price &lt; $Avg]">
        <tr>
          <td>
            <xsl:value-of select="title" />
          </td>
          <td>
            <xsl:value-of select="artist" />
          </td>
          <td>
            <xsl:value-of select="price" />
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

</xsl:stylesheet>
