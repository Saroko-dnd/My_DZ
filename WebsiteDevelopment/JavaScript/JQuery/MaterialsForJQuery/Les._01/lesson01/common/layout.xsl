<?xml version="1.0" encoding="windows-1251" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template name="hdr">
		<xsl:param name="next" select="index.xml" />
		<xsl:param name="prev" select="index.xml" />
			
		<table width="100%" align="center" cellpadding="0" cellspacing="0" style="margin:0; padding:0">
			<tr>
				<td width="30%"><a class="hdr" ><xsl:attribute name="href"><xsl:value-of select="$prev" /></xsl:attribute>Предыдущая</a></td>
				<td width="30%"><a class="hdr" href="index.xml">Оглавление</a></td>
				<td width="30%"><a class="hdr" ><xsl:attribute name="href"><xsl:value-of select="$next" /></xsl:attribute>Следующая</a></td>
			</tr>
		</table>
	</xsl:template>

	<xsl:template name="getTip">
		<xsl:param name="name" />
		<xsl:choose>
			<xsl:when test="$name = 'html'"><xsl:text>Корневой элемент страницы</xsl:text></xsl:when>
			<xsl:when test="$name = 'head'"><xsl:text>Раздел заголовка страницы</xsl:text></xsl:when>
			<xsl:when test="$name = 'body'"><xsl:text>Тело страницы</xsl:text></xsl:when>
			<xsl:when test="$name = 'a'"><xsl:text>Гиперссылка</xsl:text></xsl:when>
			<xsl:when test="$name = 'h3'"><xsl:text>Заголовок уровня 3</xsl:text></xsl:when>
			<xsl:when test="$name = 'img'"><xsl:text>Вставка изображения</xsl:text></xsl:when>
			<xsl:when test="$name = 'script'"><xsl:text>Ограничитель текста сценария</xsl:text></xsl:when>
			<xsl:when test="$name = 'noscript'"><xsl:text>Используется для вывода сообшения для браузеров, не поддерживающих JavaScript</xsl:text></xsl:when>
			
			<xsl:when test="$name = 'type' or $name = ' type'"><xsl:text>1) для скрипта указывает язык пограммирования; 2) для таблицы стилей указвает тип</xsl:text></xsl:when>
			<xsl:when test="$name = 'src' or $name = ' src'"><xsl:text>1)для скрипта указывает местоположение файла с текстом сценария; 2) для изображения указывает местоположение файла изображения</xsl:text></xsl:when>
			<xsl:when test="$name = 'href' or $name = ' href'"><xsl:text>Адрес заглужаемого по ссылке ресурса</xsl:text></xsl:when>
			<xsl:when test="$name = 'var'"><xsl:text>Объявление переменной</xsl:text></xsl:when>
			<xsl:when test="$name = 'true'"><xsl:text>Истина</xsl:text></xsl:when>
			<xsl:when test="$name = 'false'"><xsl:text>Ложь</xsl:text></xsl:when>

			<xsl:when test="$name = 'undefined'"><xsl:text>Неопределенное значение</xsl:text></xsl:when>
			<xsl:when test="$name = 'null'"><xsl:text>Пустое значение</xsl:text></xsl:when>
			<xsl:when test="$name = 'alert'"><xsl:text>Диалоговое окно "Предупреждение"</xsl:text></xsl:when>
			<xsl:when test="$name = 'if'"><xsl:text>Конструкция ветвления</xsl:text></xsl:when>
			<xsl:when test="$name = 'else'"><xsl:text>Альтернативная ветвь конструкции ветвления</xsl:text></xsl:when>
			<xsl:when test="$name = 'switch'"><xsl:text>Конструкция выбора из множества альтернатив</xsl:text></xsl:when>
			<xsl:when test="$name = 'case'"><xsl:text>Ветвь конструкции выбора</xsl:text></xsl:when>
			<xsl:when test="$name = 'default'"><xsl:text>Альтернативная ветвь конструкции выбора</xsl:text></xsl:when>
			<xsl:when test="$name = 'break'"><xsl:text>Прерывание выбора или цикла</xsl:text></xsl:when>
			<xsl:when test="$name = 'continue'"><xsl:text>Прерывание текущей итерации цикла</xsl:text></xsl:when>
			<xsl:when test="$name = 'for'"><xsl:text>Цикл со счетчиком</xsl:text></xsl:when>
			<xsl:when test="$name = 'while'"><xsl:text>Цикл с неизвестным количеством повторений</xsl:text></xsl:when>
			<xsl:when test="$name = 'function'"><xsl:text>Объявление функции</xsl:text></xsl:when>
			<xsl:when test="$name = 'return'"><xsl:text>Возврат значения из функции</xsl:text></xsl:when>
			<xsl:when test="$name = 'this'"><xsl:text>Внутренний указатель объекта</xsl:text></xsl:when>
			<xsl:when test="$name = 'new'"><xsl:text>Оператор создания объекта</xsl:text></xsl:when>
			<xsl:when test="$name = 'parseInt'"><xsl:text>Преобразование строки в целое число</xsl:text></xsl:when>
			<xsl:when test="$name = 'parseFloat'"><xsl:text>Преобразование строки в число с плавающей точкой</xsl:text></xsl:when>
			<xsl:when test="$name = 'Array'"><xsl:text>Объект "Массив"</xsl:text></xsl:when>
			<xsl:when test="$name = 'String'"><xsl:text>Объект "Строка"</xsl:text></xsl:when>
			<xsl:when test="$name = 'Math'"><xsl:text>Объект, реализующий математический аппарат в JavaSctipr</xsl:text></xsl:when>
			<xsl:when test="$name = 'Date'"><xsl:text>Объект, реализующий работу с датой и временем в JavaSctipr</xsl:text></xsl:when>


		</xsl:choose>
	</xsl:template>

<!-- правило для корневого элемента -->
	<xsl:template match="/">
		<html>
			<head>
				<title>
					<xsl:value-of select="lesson/@title" />
				</title>
				<link rel="stylesheet" type="text/css" href="common/style.css" />
				<script language="javascript" src="common/script.js"></script>
				<script>
					//window.resizeTo(1024,768);
					//window.defaultStatus = "(c) Артемов АН, 2004г.";
				</script>
			</head>
			<body>
				<xsl:call-template name="hdr">
					<xsl:with-param name="next" select="lesson/@next" />
					<xsl:with-param name="prev" select="lesson/@prev" />
				</xsl:call-template>
				<hr class="main" />
			<!-- содержимое  -->
			
				<xsl:apply-templates />
				
				<xsl:if test="number(lesson/@summ)&gt;0 and (count(//kbshort)&gt;0 or count(//example/@href)&gt;0)">
					<hr class="main" />
					<xsl:if test="count(//kbshort)&gt;0">
						<h4>Используемые комбинации клавиш</h4>
						<xsl:apply-templates select="//kbshort" mode="table" />
					</xsl:if>
					
					<xsl:if test="count(//example/@href)&gt;0">
						<h3>Все примеры</h3>
						<xsl:apply-templates select="//example" mode="short" />
					</xsl:if>
				</xsl:if>				
			<!-- /содержимое  -->
			
				<hr class="main" />
				<xsl:call-template name="hdr">
					<xsl:with-param name="next" select="lesson/@next" />
					<xsl:with-param name="prev" select="lesson/@prev" />
				</xsl:call-template>				
			</body>
		</html>
	</xsl:template>
	
	<!-- примеры кода (нормальный режим)-->
	<xsl:template match="example">
		
		<table width="100%" cellpadding="0" cellspacing="0" class="example">
			
			<xsl:if test="boolean(./@number)">
				<tr class="headline">
					<td class="title">						
						Листинг №<xsl:value-of select="./@number" />: <xsl:value-of select="./@title" />
					</td>
					<td class="controls">						
					</td>
				</tr>				
			</xsl:if>
			
			<tbody class="examplebody">
				<xsl:attribute name="id"><xsl:value-of select="concat('example', ./@number)"/></xsl:attribute>
				<tr>
					<td class="code" colspan="2">
						<pre><xsl:apply-templates /></pre></td>
				</tr>
				<xsl:if test="boolean(./@href)">
					<tr>
						<td>					
							<a>
								<xsl:attribute name="href"><xsl:value-of select="./@href" /></xsl:attribute>
								<xsl:attribute name="target">
									<xsl:choose>
										<xsl:when test="./@newwin = 'no'">_self</xsl:when>
										<xsl:otherwise>_blanc</xsl:otherwise>
									</xsl:choose>
								</xsl:attribute>
								<img src="common/run2.gif" border="0" align="absmiddle" style="margin: 2 3 0 0;"/>выполнить</a>.					
						</td>		
					</tr>
				</xsl:if>
			</tbody>
		</table>
	</xsl:template>

	<!-- примеры кода (сокращенный режим)-->
	<xsl:template match="example" mode="short">
		<xsl:if test="boolean(./@href)">
			<table width="100%" cellpadding="0" cellspacing="0"  style="margin-left: 20; margin-top: 0; margin-bottom: 0;">
				<tr>
					<td width="60">					
						<a>
							<xsl:attribute name="href"><xsl:value-of select="./@href" /></xsl:attribute>
							<xsl:attribute name="target">
								<xsl:choose>
									<xsl:when test="./@newwin = 'no'">_self</xsl:when>
									<xsl:otherwise>_blanc</xsl:otherwise>
								</xsl:choose>
							</xsl:attribute>
							<img src="common/run2.gif" border="0" align="absmiddle" style="margin: 2 3 0 0;"/>выполнить</a>.&#160;
					</td>		
					<td>Пример №<xsl:value-of select="./@number" />: <xsl:value-of select="./@title" />
					</td>
				</tr>
			</table>
		</xsl:if>		
	</xsl:template>
	
<!-- правила для элементов форматирования примеров исходного кода -->
	<!-- ключевое слово -->
	<xsl:template match="keyword|keyw|kw">
		<strong class="keyword">
			<xsl:attribute name="title">
				<xsl:call-template name="getTip">
					<xsl:with-param name="name" select="."/>
				</xsl:call-template>
			</xsl:attribute>
			<xsl:apply-templates /></strong>
	</xsl:template>

	<!-- зарезервированное слово -->
	<xsl:template match="resword|rword|reserved|rw">
		<strong class="resword">
			<xsl:attribute name="title">
				<xsl:call-template name="getTip">
					<xsl:with-param name="name" select="."/>
				</xsl:call-template>
			</xsl:attribute>
			<xsl:apply-templates /></strong>
	</xsl:template>

	<!-- стандартная функция -->
	<xsl:template match="stdfunc|sf">
		<span class="stdfunc">
			<xsl:attribute name="title">
				<xsl:call-template name="getTip">
					<xsl:with-param name="name" select="."/>
				</xsl:call-template>
			</xsl:attribute>
			<xsl:apply-templates /></span>
	</xsl:template>
	
	<!-- тэг HTML -->
	<xsl:template match="tag">
		<strong class="tag">
			<xsl:attribute name="title">
				<xsl:call-template name="getTip">
					<xsl:with-param name="name" select="."/>
				</xsl:call-template>
			</xsl:attribute>
			<xsl:apply-templates /></strong>
	</xsl:template>

	<!-- атрибут элемента HTML -->
	<xsl:template match="attr">
		<span class="attr">
			<xsl:attribute name="title">
				<xsl:call-template name="getTip">
					<xsl:with-param name="name" select="."/>
				</xsl:call-template>
			</xsl:attribute>
			<xsl:apply-templates /></span>
	</xsl:template>
	
	<!-- строковый литерал -->
	<xsl:template match="str">
		<span class="str"><xsl:apply-templates /></span>
	</xsl:template>

	<!-- числовой литерал -->
	<xsl:template match="num">
		<span class="num"><xsl:apply-templates /></span>
	</xsl:template>

	<!-- комментарий -->
	<xsl:template match="comment">
		<span class="comment"><xsl:apply-templates /></span>
	</xsl:template>
	
<!-- сочетание клавиш -->
	<!-- обычный режим (выводится только сочетание, при наведении вспл. подсказка) -->
	<xsl:template match="kbshort">		
		<strong class="kbshort">
			<xsl:attribute name="title">
				<xsl:value-of select="./@title" />
			</xsl:attribute>
			<xsl:value-of select="." />
		</strong>
	</xsl:template>

	<!-- расширеннвй, табличный режим (выводится сочетание и пояснение действия) -->
	<xsl:template match="kbshort" mode="table">		
		<table width="100%" cellpadding="0" cellspacing="0" style="margin-left: 20; margin-top: 0; margin-bottom: 0;">
			<tr>
				<colgroup class="shortcut"/>
				<td width="120"><xsl:value-of select="." /></td>
				<td><xsl:value-of select="./@title" /></td>
			</tr>			
		</table>
	</xsl:template>

<!-- совет -->
	<xsl:template match="tip">
		<div class="tip"><strong>
		<xsl:if test="number(./start) != 0"><xsl:value-of select="./@start" />: </xsl:if></strong><xsl:apply-templates /></div>
	</xsl:template>

<!-- определение -->
	<xsl:template match="definition|def">
		<strong><xsl:value-of select="./@term" /></strong><xsl:apply-templates />
	</xsl:template>

<!-- изображение -->
	<xsl:template match="picture">
		<div class="img">
			<img>
				<xsl:apply-templates select="./@src" />				
			</img>
			<br />
			<span class="subscript">Рис.&#160;№<xsl:value-of select="./@number" />.&#160;<xsl:value-of select="./@title" /></span>
		</div>
	</xsl:template>

	<xsl:template match="p">
		<p><xsl:apply-templates /></p>
		<xsl:if test="number(./@break)=1">
			<br clear="all" />
		</xsl:if>
	</xsl:template>
	
	<xsl:template match="menu">
		<u><xsl:apply-templates /></u>
	</xsl:template>	
	
	<xsl:template match="highlight">
		<span class="highlight">
			<xsl:apply-templates />
		</span>
	</xsl:template>

	<xsl:template match="@*">
		<xsl:attribute name="{name()}">			
			<xsl:value-of select="." />
		</xsl:attribute>
	</xsl:template>	

<!-- правило для элементов, которые не подошли ни под одно из вышеописанных правил -->
	<xsl:template match="node()">
		<xsl:copy>
			<xsl:apply-templates select="@*" />			
			<xsl:apply-templates /></xsl:copy>
	</xsl:template>
</xsl:stylesheet>