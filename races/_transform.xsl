<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="Race">
  <html>
  <head>
  <title>Viewing <xsl:value-of select="Name" /> Race</title>
  </head>
  <body  bgcolor="#000000" text="#c0c0c0" link="#fcaa21" vlink="#fcaa21" alink="#fcaa21">
<font face="Geneva, Arial, Helvetica, san-serif">
  <h2>Viewing Race: <xsl:value-of select="Name" /></h2>
  <p>Size: <xsl:value-of select="Size" /></p>
  <p>Language: <xsl:value-of select="Language" /></p>
  <p>Classes Available: <xsl:value-of select="ClassesAvailable" /></p>
  <p>Base Height: <xsl:value-of select="Height" /> inches</p>
  <p>Base Weight: <xsl:value-of select="Weight" /> pounds</p>
  <p>Base Age: <xsl:value-of select="BaseAge" /> years</p>
  <p>Max Age: <xsl:value-of select="MaxAge" /> years</p>
  <p>Base Alignment: <xsl:value-of select="BaseAlignment" /></p>
  <p>Max Strength: <xsl:value-of select="StrModifier" /></p>
  <p>Max Intelligence: <xsl:value-of select="IntModifier" /></p>
  <p>Max Wisdom: <xsl:value-of select="WisModifier" /></p>
  <p>Max Dexterity: <xsl:value-of select="DexModifier" /></p>
  <p>Max Constitution: <xsl:value-of select="ConModifier" /></p>
  <p>Max Charisma: <xsl:value-of select="ChaModifier" /></p>
  <p>Max Agility: <xsl:value-of select="AgiModifier" /></p>
  <p>Max Power: <xsl:value-of select="PowModifier" /></p>
  <p>Body Parts: <xsl:value-of select="BodyParts" /></p>
  <p>Resistant To: <xsl:value-of select="Resistant" /></p>
  <p>Immune To: <xsl:value-of select="Immune" /></p>
  <p>Susceptible To: <xsl:value-of select="Susceptible" /></p>
  <p>Vulnerable To: <xsl:value-of select="Vulnerable" /></p>
<a href="http://basternae.org/races.html">Back to races</a>
</font>
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>
