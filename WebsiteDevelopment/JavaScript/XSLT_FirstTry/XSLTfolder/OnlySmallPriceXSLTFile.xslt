<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <h2>My CD Collection (price &lt; 10)</h2>
    <table border="1">
      <tr bgcolor="#9acd32">
        <th style="text-align:left">Title</th>
        <th style="text-align:left">Artist</th>
        <th style="text-align:left">Price</th>
      </tr>
      <xsl:for-each select="catalog/cd[price &lt; 10]">
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

