<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:template match="/AllImages">
          <xsl:for-each select="img">
            <img src="{src}"/>         
          </xsl:for-each>
  </xsl:template>
  
</xsl:stylesheet>
