package;

import kha.Image;
import kha.graphics2.Graphics;
import kha.Color;

using kha.graphics2.GraphicsExtension;
using kha.graphics2.Graphics1;

enum BunnyFlavour {
	BUNNY;
	TEXT;
	RECTANGLE;
	CIRCLE;
	TRIANGLE;
	LINE;
	POINT;
}

class AbstractBunny {
  
  public var x: Float;
  
  public function getX() : Float {
	  return x;
  }
  
  public var y: Float;

  public function getY() : Float {
	  return y;
  }
  
  public function teleport(x: Float, y: Float) : Void {
	  if (x < 0 || y < 0) {
		  return;
	  }
	  this.x = x;
	  this.y = y;
  }
  
  private var speedX: Float;
  
  public function setSpeedX(speedX: Float) : Void {
	  this.speedX = speedX;
  }
  
  private var speedY: Float;
  
  public function setSpeedY(speedY: Float) : Void {
	  this.speedY = speedY;
  }
  
  private var rotation: Float;
  
  public function getRotation() : Float {
	  return rotation;
  }
  
  public function setRotation(rotation: Float) : Void {
	  this.rotation = Math.PI / 180 * rotation;
  }
  
  public function rotate(rotation: Float) : Void {
	  this.rotation += Math.PI / 180 * rotation;
  }
  
  private var scaleX : Float;
  
  public function getScaleX() : Float {
	  return scaleX;
  }
  
  private var scaleY : Float;
  
  public function getScaleY() : Float {
	  return scaleY;
  }
  
  public function setScale(scaleX : Float, scaleY : Float) : Void {
	  this.scaleX = scaleX;
	  this.scaleY = scaleY;
  }
  
  private var growth : Float;
  
  public function getGrowth() : Float {
	  return growth;
  }
  
  public function setGrowth(growth : Float) {
	  this.growth = growth;
  }
  
  public function grow() {
	  setScale(scaleX + growth, scaleY + growth);
  }
  
  public function new() {
	this.scaleX = 1;
	this.scaleY = 1;
	this.growth = 0.1;
	this.rotation = 0;
	this.color = Color.White;
  }
  
  public function jump(gravity: Float, minX: Float, minY: Float, maxX: Float, maxY: Float) : Void {
	  this.x += this.speedX;
	  this.y += this.speedY;
	  this.speedY += gravity;
	  
	  if (this.x < minX) {
		  this.speedX *= -1;
		  this.x = minX;
	  } else if (this.x > maxX) {
		  this.speedX *= -1;
		  this.x = maxX;
	  }
	  
	  if (this.y > maxY) {
		  this.speedY *= -0.8;
		  if (Std.random(2) == 1) {
			  this.speedY -= 3 + Math.random() * 4;
		  }
		  this.y = maxY;
	  } else if (this.y < minY) {
		  this.speedY = 0;
		  this.y = minY;
	  }
  }

  private var color : Color;
  
  public function setColor(color : Color) {
	  this.color = color;
  }
  
  public function getColor() {
	  return color;
  }
  
  public function render(g : Graphics) {};
  
  public function setTexture(t : Image) {
	  throw "Unsupported operation";
  }
}

class Bunny extends AbstractBunny {
	private var texture: Image;

	public override function setTexture(texture: Image) {
		this.texture = texture;
	}
	
	public function new(texture : Image) {
		super();
    	this.texture = texture;
	}
	
	public override function render(g: Graphics) {
		var downScaleFactor = Math.min(BunnymarkUtils.getMaxTextureHeight(), texture.realHeight) / texture.realHeight;

		var dx = downScaleFactor * scaleX * texture.realWidth;
		var dy = downScaleFactor * scaleY * texture.realHeight;
		g.color = color;
		g.rotate(rotation, x + dx / 2, y + dy / 2);
		g.drawScaledImage(texture, x, y, dx, dy);
		g.rotate(-rotation, x + dx / 2, y + dy / 2);
	}
}

class Circle extends AbstractBunny {
	private var radius : Int;
	
	public function new(radius : Int, color : Color) {
		super();
		this.radius = radius;
		this.color = color;
	}
	
	public override function teleport(x : Float, y : Float) {
		super.teleport(x+radius, y+radius);
	}
	
	public override function render(g : Graphics) {
		g.color = color;
		g.drawCircle(x, y, radius * scaleX);
	}
}

class Rectangle extends AbstractBunny {
	private var width : Int;
	private var height : Int;
	
	public function new(width : Int, height : Int, color : Color) {
		super();
		this.width = width;
		this.height = height;
		this.color = color;
	}
	
	public override function render(g : Graphics) {
		var dx = width * scaleX;
		var dy = height * scaleY;
		
		g.color = color;
		g.rotate(rotation, x + height/2, y + width/2);
		g.drawRect(x - dx / 2, y - dy / 2, dx, dy);
		g.rotate(-rotation, x + height/2, y + width/2);
	}
}

class Text extends AbstractBunny {
	private var text : String;
	
	public function new(text : String, color : Color) {
		super();
		this.text = text;
		this.color = color;
	}
	
	public override function render(g : Graphics) {
		g.fontSize = Math.round(20 * scaleX);
		g.color = color;
		g.rotate(rotation, x, y);
		g.drawString(text, x, y);
		g.rotate(-rotation, x, y);
	}
}

class Triangle extends AbstractBunny {
	private var x1 : Int;
	private var y1 : Int;
	private var x2 : Int;
	private var y2 : Int;
	private var x3 : Int;
	private var y3 : Int;
	
	public function new(x1 : Int, y1 : Int, x2 : Int, y2 : Int, x3 : Int, y3 : Int, color : Color) {
		super();
		this.x1 = x1;
		this.y1 = y1;
		this.x2 = x2;
		this.y2 = y2;
		this.x3 = x3;
		this.y3 = y3;
		this.color = color;
	}
	
	public override function render(g : Graphics) {
		var centerX = ((x1 + x2 + x3) * getScaleX() / 3.0) + x;
		var centerY = ((y1 + y2 + y3) * getScaleY() / 3.0) + y;
		g.color = color;
		g.rotate(rotation, centerX, centerY);
		g.drawPolygon(x,y, [
			new kha.math.Vector2(x1*getScaleX(),y1*getScaleY()), 
			new kha.math.Vector2(x2*getScaleX(),y2*getScaleY()), 
			new kha.math.Vector2(x3*getScaleX(),y3*getScaleY())]);
		g.rotate(-rotation, centerX, centerY);
		g.color = Color.White;
	}
}

class Point extends AbstractBunny {
	
	public function new(color : Color) {
		super();
		this.color = color;
	}
	
	public override function render(g : Graphics) {
		g.color = color;
		// graphics2.Graphics does not seem to support setPixel()
		g.drawLine(x,y,x+1,y);
	}
}

class Line extends AbstractBunny {
	private var deltaX : Int;
	private var deltaY : Int;
	
	public function new(deltaX : Int, deltaY : Int, color : Color) {
		super();
		this.color = color;
		this.deltaX = deltaX;
		this.deltaY = deltaY;
	}
	
	public override function render(g : Graphics) {
		var dx = (scaleX * deltaX) / 2.0;
		var dy = (scaleY * deltaY) / 2.0;
		g.color = color;
		g.rotate(rotation, x, y);
		g.drawLine(x - dx, y - dy, x + dx, y + dy);
		g.rotate(-rotation, x, y);
	}
}