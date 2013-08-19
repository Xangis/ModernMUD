<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="CharClass">
  <html>
  <head>
  <title>Viewing <xsl:value-of select="Name" /> Character Class</title>
  </head>
  <body  bgcolor="#000000" text="#c0c0c0" link="#fcaa21" vlink="#fcaa21" alink="#fcaa21">
<font face="Geneva, Arial, Helvetica, san-serif">
  <h2>Character Class: <xsl:value-of select="Name" /></h2>
  <p>Prime Attribute: <xsl:value-of select="PrimeAttribute" /></p>
  <p>Base Hitpoint Gain: <xsl:value-of select="MinHpGain" /> - <xsl:value-of select="MaxHpGain" /></p>
  <p>Skills:</p>
  <xsl:for-each select="Skills">
  <table>
  <xsl:for-each select="SkillEntry">
  <xsl:sort select="Level" data-type="number"/>
  <tr>
  <td><xsl:value-of select="Name" > </xsl:value-of></td>
  <td>Level: <xsl:value-of select="Level"></xsl:value-of></td>
  </tr></xsl:for-each>
  </table>
  </xsl:for-each>
  <p>Spells: </p>
  <xsl:for-each select="Spells">
  <table>
  <xsl:for-each select="SpellEntry">
  <xsl:sort select="Circle" data-type="number"/>
 <tr><td><xsl:value-of select="Name"></xsl:value-of></td>
  <td>Circle: <xsl:value-of select="Circle"></xsl:value-of></td>
  </tr></xsl:for-each>
  </table>
  </xsl:for-each>
<a href="http://basternae.org/classes.html">Back to classes</a>
</font>
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>
